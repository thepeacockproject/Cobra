using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Cobra.Server.Interfaces;
using Cobra.Server.Models;
using Cobra.Server.Shared.Models;

namespace Cobra.Server.Mvc
{
    public class SteamAuthMiddleware : ISteamAuthMiddleware
    {
        private sealed class JwtToken
        {
            public class JwtTokenPayload
            {
                public ulong Uid { get; init; }
                public long Timestamp { get; init; }
            }

            public JwtTokenPayload Payload { get; init; }
            public string Hash { get; init; }
        }

        private static class RequestHeaders
        {
            public const string OSAuthProvider = "OS-AuthProvider";
            public const string OSAuthTicketData = "OS-AuthTicketData";
            public const string OSUID = "OS-UID";
            public const string OSAuthResponse = "OS-AuthResponse";
            public const string OSError = "OS-Error";
        }

        private const string OSAuthProviderServer = "6";

        private static class OSErrors
        {
            //public const int OK = 0;
            public const int Failed = 1;
            public const int Expired = 2;
        }

        private readonly ISteamService _steamService;
        private readonly int _jwtTokenExpirationInSeconds;
        private readonly byte[] _jwtSignKey;

        public SteamAuthMiddleware(ISteamService steamService, Options options)
        {
            _steamService = steamService;
            _jwtTokenExpirationInSeconds = options.JwtTokenExpirationInSeconds;
            _jwtSignKey = Encoding.UTF8.GetBytes(options.JwtSignKey);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (
                !context.Request.Path.HasValue ||
                (
                    !context.Request.Path.Value.StartsWith("/hm5") &&
                    !context.Request.Path.Value.StartsWith("/sniper")
                )
            )
            {
                await next(context);

                return;
            }

            var authProvider = context.Request.Headers[RequestHeaders.OSAuthProvider];

            if (!TryDecodeBase64String(context.Request.Headers[RequestHeaders.OSAuthTicketData], out var authTicketData))
            {
                RejectRequest(context, OSErrors.Failed);

                return;
            }

            if (!ulong.TryParse(context.Request.Headers[RequestHeaders.OSUID], out var steamId))
            {
                RejectRequest(context, OSErrors.Failed);

                return;
            }

            //NOTE: Wrap in a try-catch to make sure unhandled situations result in a rejected response
            try
            {
                //NOTE: Check for our own AuthProvider first
                if (authProvider == OSAuthProviderServer)
                {
                    var jwtToken = DecodeSimpleJwtToken(authTicketData);

                    if (
                        jwtToken != null &&
                        jwtToken.Uid == steamId &&
                        DateTimeOffset.Now.ToUnixTimeSeconds() - jwtToken.Timestamp < _jwtTokenExpirationInSeconds
                    )
                    {
                        AuthenticateRequest(context, jwtToken.Uid);

                        await next(context);

                        return;
                    }

                    RejectRequest(context, OSErrors.Expired);

                    return;
                }

                //NOTE: If we didn't pass the previous check, enforce valid AuthTicketData.
                var result = await _steamService.AuthenticateUser(authTicketData, steamId);

                if (result)
                {
                    var jwtToken = EncodeSimpleJwtToken(new JwtToken.JwtTokenPayload
                    {
                        Uid = steamId,
                        Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
                    });

                    context.Response.Headers[RequestHeaders.OSAuthResponse] = jwtToken;

                    AuthenticateRequest(context, steamId);

                    await next(context);

                    return;
                }
            }
            catch
            {
                //Do nothing
            }

            //NOTE: Always reject a request at this point
            RejectRequest(context, OSErrors.Failed);
        }

        private static void AuthenticateRequest(HttpContext context, ulong steamId)
        {
            context.User = new ClaimsPrincipal(new CustomIdentity(steamId));
        }

        private static void RejectRequest(HttpContext context, int osError)
        {
            context.Response.StatusCode = 403;
            context.Response.Headers[RequestHeaders.OSError] = osError.ToString();
        }

        private static bool TryDecodeBase64String(string base64String, out byte[] bytes)
        {
            if (base64String == null)
            {
                bytes = null;

                return false;
            }

            Span<byte> bytesBuffer = stackalloc byte[base64String.Length];

            if (
                !Convert.TryFromBase64String(base64String, bytesBuffer, out var bytesWritten) ||
                bytesWritten == 0
            )
            {
                bytes = null;

                return false;
            }

            bytes = bytesBuffer[..bytesWritten].ToArray();

            return true;
        }

        private string EncodeSimpleJwtToken(JwtToken.JwtTokenPayload payload)
        {
            using var hasher = new HMACSHA256(_jwtSignKey);

            var hash = Convert.ToHexString(hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(
                        JsonSerializer.Serialize(payload)
                    )
                )
            );

            var jwtToken = new JwtToken
            {
                Payload = payload,
                Hash = hash
            };

            return Convert.ToBase64String(
                Encoding.UTF8.GetBytes(
                    JsonSerializer.Serialize(jwtToken)
                )
            );
        }

        private JwtToken.JwtTokenPayload DecodeSimpleJwtToken(byte[] simpleJwtToken)
        {
            var jwtToken = JsonSerializer.Deserialize<JwtToken>(
                Encoding.UTF8.GetString(simpleJwtToken)
            );

            using var hasher = new HMACSHA256(_jwtSignKey);

            var hash = Convert.ToHexString(hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(
                        JsonSerializer.Serialize(jwtToken.Payload)
                    )
                )
            );

            return jwtToken.Hash == hash ? jwtToken.Payload : null;
        }
    }
}
