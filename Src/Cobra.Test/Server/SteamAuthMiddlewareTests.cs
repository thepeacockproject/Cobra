using System.Diagnostics.CodeAnalysis;
using System.Text;
using Cobra.Server.Interfaces;
using Cobra.Server.Models;
using Cobra.Server.Mvc;
using Cobra.Server.Shared.Models;
using Cobra.Test.Extensions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Cobra.Test.Server
{
    public class SteamAuthMiddlewareTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("/")]
        [InlineData("/test")]
        [InlineData("/unknown")]
        public async Task PathWithUnknownPrefix_ShouldBe_Skipped(string path)
        {
            var (_, steamAuthMiddleware, httpContext) = SetupInstances(path);

            HttpContext nextContext = null;

            await steamAuthMiddleware.InvokeAsync(httpContext, context =>
            {
                nextContext = context;

                return Task.CompletedTask;
            });

            Assert.NotNull(nextContext);
            Assert.Equal(httpContext, nextContext);
            Assert.False(httpContext.User.Identity?.IsAuthenticated);
            Assert.Equal(200, httpContext.Response.StatusCode);
            Assert.False(httpContext.Response.Headers.ContainsKey("OS-Error"));
        }

        [Theory]
        [InlineData(null, 47)]
        [InlineData("unknown", 47)]
        [InlineData("unknown", 101)]
        [InlineData("unknown", 1337)]
        public async Task UnknownAuthProviderWithValidAuthentication_ShouldBe_Authenticated(string authProvider, ulong userId)
        {
            var (steamServiceMock, steamAuthMiddleware, httpContext) = SetupInstances(
                "/hm5",
                authProvider: authProvider,
                authTicketData: Convert.ToBase64String(Encoding.UTF8.GetBytes(userId.ToString())),
                uid: userId.ToString()
            );

            var steamServiceExpression = steamServiceMock.Expression(x => x.AuthenticateUser(
                It.IsAny<byte[]>(),
                It.IsAny<ulong>()
            ));

            steamServiceMock.Setup(steamServiceExpression).ReturnsAsync(true);

            HttpContext nextContext = null;

            await steamAuthMiddleware.InvokeAsync(httpContext, context =>
            {
                nextContext = context;

                return Task.CompletedTask;
            });

            steamServiceMock.Verify(steamServiceExpression, Times.Once);

            Assert.NotNull(nextContext);
            Assert.Equal(httpContext, nextContext);
            Assert.True(httpContext.User.Identity!.IsAuthenticated);
            Assert.Equal(userId, ((CustomIdentity)httpContext.User.Identity).SteamId);
            Assert.True(httpContext.Response.Headers.ContainsKey("OS-AuthResponse"));
            Assert.Equal(200, httpContext.Response.StatusCode);
            Assert.False(httpContext.Response.Headers.ContainsKey("OS-Error"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid")]
        public async Task UnknownAuthProviderWithInvalidAuthTicketData_ShouldBe_Rejected(string authTicketData)
        {
            await TestForRejectedUnknownAuthProvider(
                "47",
                authTicketData
            );
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid")]
        public async Task UnknownAuthProviderWithInvalidUserId_ShouldBe_Rejected(string userId)
        {
            await TestForRejectedUnknownAuthProvider(
                userId,
                Convert.ToBase64String(Encoding.UTF8.GetBytes("47"))
            );
        }

        [Theory]
        [InlineData(47)]
        [InlineData(101)]
        [InlineData(1337)]
        public async Task UnknownAuthProviderWithAuthenticationForOtherUser_ShouldBe_Rejected(ulong userId)
        {
            await TestForRejectedUnknownAuthProvider(
                userId.ToString(),
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes((userId + 1).ToString())
                )
            );
        }

        [Theory]
        [InlineData(47)]
        [InlineData(101)]
        [InlineData(1337)]
        public async Task KnownAuthProviderWithValidAuthentication_ShouldBe_Authenticated(ulong userId)
        {
            var authTicketData = await GetAuthTicketDataForUserId(userId);

            var (_, steamAuthMiddleware, httpContext) = SetupInstances(
                "/hm5",
                authProvider: "6",
                authTicketData: authTicketData,
                uid: userId.ToString(),
                jwtTokenExpirationInSeconds: int.MaxValue
            );

            HttpContext nextContext = null;

            await steamAuthMiddleware.InvokeAsync(httpContext, context =>
            {
                nextContext = context;

                return Task.CompletedTask;
            });

            Assert.NotNull(nextContext);
            Assert.Equal(httpContext, nextContext);
            Assert.True(httpContext.User.Identity!.IsAuthenticated);
            Assert.Equal(userId, ((CustomIdentity)httpContext.User.Identity).SteamId);
            Assert.False(httpContext.Response.Headers.ContainsKey("OS-AuthResponse"));
            Assert.Equal(200, httpContext.Response.StatusCode);
            Assert.False(httpContext.Response.Headers.ContainsKey("OS-Error"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid")]
        public async Task KnownAuthProviderWithInvalidAuthTicketData_ShouldBe_Rejected(string authTicketData)
        {
            await TestForRejectedKnownAuthProvider(
                "47",
                authTicketData,
                int.MaxValue,
                "1"
            );
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid")]
        public async Task KnownAuthProviderWithInvalidUserId_ShouldBe_Rejected(string userId)
        {
            var authTicketData = await GetAuthTicketDataForUserId(47);

            await TestForRejectedKnownAuthProvider(
                userId,
                authTicketData,
                int.MaxValue,
                "1"
            );
        }

        [Theory]
        [InlineData(47)]
        [InlineData(101)]
        [InlineData(1337)]
        public async Task KnownAuthProviderWithAuthenticationForOtherUser_ShouldBe_Rejected(ulong userId)
        {
            var authTicketData = await GetAuthTicketDataForUserId(userId + 1);

            //NOTE: We could argue that a user mismatch should be error code 1 instead
            await TestForRejectedKnownAuthProvider(
                userId.ToString(),
                authTicketData,
                int.MaxValue,
                "2"
            );
        }

        [Theory]
        [InlineData(47)]
        [InlineData(101)]
        [InlineData(1337)]
        public async Task KnownAuthProviderWithExpiredAuthentication_ShouldBe_Rejected(ulong userId)
        {
            var authTicketData = await GetAuthTicketDataForUserId(userId);

            await TestForRejectedKnownAuthProvider(
                userId.ToString(),
                authTicketData,
                0,
                "2"
            );
        }

        private static (
            Mock<ISteamService> steamServiceMock,
            SteamAuthMiddleware steamAuthMiddleware,
            DefaultHttpContext httpContext
        ) SetupInstances(
            string path,
            string authProvider = "",
            string authTicketData = "",
            string uid = "",
            int jwtTokenExpirationInSeconds = 0,
            string jwtSignKey = "test"
        )
        {
            var steamServiceMock = new Mock<ISteamService>();

            var steamAuthMiddleware = new SteamAuthMiddleware(
                steamServiceMock.Object,
                new Options
                {
                    JwtTokenExpirationInSeconds = jwtTokenExpirationInSeconds,
                    JwtSignKey = jwtSignKey
                }
            );

            var httpContext = new DefaultHttpContext
            {
                Request =
                {
                    Path = path,
                    Headers =
                    {
                        { "OS-AuthProvider", authProvider},
                        { "OS-AuthTicketData", authTicketData},
                        { "OS-UID", uid}
                    }
                }
            };

            return (steamServiceMock, steamAuthMiddleware, httpContext);
        }

        private static async Task TestForRejectedUnknownAuthProvider(
            string userId,
            string authTicketData
        )
        {
            var (steamServiceMock, steamAuthMiddleware, httpContext) = SetupInstances(
                "/hm5",
                authProvider: "unknown",
                uid: userId,
                authTicketData: authTicketData
            );

            var steamServiceExpression = steamServiceMock.Expression(x => x.AuthenticateUser(
                It.IsAny<byte[]>(),
                It.IsAny<ulong>()
            ));

            steamServiceMock.Setup(steamServiceExpression).ReturnsAsync(false);

            HttpContext nextContext = null;

            await steamAuthMiddleware.InvokeAsync(httpContext, [ExcludeFromCodeCoverage] (context) =>
            {
                nextContext = context;

                return Task.CompletedTask;
            });

            steamServiceMock.Verify(steamServiceExpression, Times.AtMost(1));

            Assert.Null(nextContext);
            Assert.False(httpContext.User.Identity?.IsAuthenticated);
            Assert.Equal(403, httpContext.Response.StatusCode);
            Assert.Equal("1", httpContext.Response.Headers["OS-Error"]);
        }

        private static async Task TestForRejectedKnownAuthProvider(
            string userId,
            string authTicketData,
            int jwtTokenExpirationInSeconds,
            string expectedErrorCode
        )
        {
            var (_, steamAuthMiddleware, httpContext) = SetupInstances(
                "/hm5",
                authProvider: "6",
                authTicketData: authTicketData,
                uid: userId,
                jwtTokenExpirationInSeconds: jwtTokenExpirationInSeconds
            );

            HttpContext nextContext = null;

            await steamAuthMiddleware.InvokeAsync(httpContext, [ExcludeFromCodeCoverage] (context) =>
            {
                nextContext = context;

                return Task.CompletedTask;
            });

            Assert.Null(nextContext);
            Assert.False(httpContext.User.Identity?.IsAuthenticated);
            Assert.Equal(403, httpContext.Response.StatusCode);
            Assert.Equal(expectedErrorCode, httpContext.Response.Headers["OS-Error"]);
        }

        //TODO: Maybe add a service for the encoding/decoding of tokens, so it can be independently used/tested/mocked?
        private static async Task<string> GetAuthTicketDataForUserId(ulong userId)
        {
            var (steamServiceMock, steamAuthMiddleware, httpContext) = SetupInstances(
                "/hm5",
                authTicketData: Convert.ToBase64String(Encoding.UTF8.GetBytes(userId.ToString())),
                uid: userId.ToString()
            );

            var steamServiceExpression = steamServiceMock.Expression(x => x.AuthenticateUser(
                It.IsAny<byte[]>(),
                It.IsAny<ulong>()
            ));

            steamServiceMock.Setup(steamServiceExpression).ReturnsAsync(true);

            await steamAuthMiddleware.InvokeAsync(httpContext, _ => Task.CompletedTask);

            return httpContext.Response.Headers["OS-AuthResponse"].Single();
        }
    }
}
