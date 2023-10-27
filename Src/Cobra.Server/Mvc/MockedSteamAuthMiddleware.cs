using System.Security.Claims;
using Cobra.Server.Interfaces;
using Cobra.Server.Models;
using Cobra.Server.Shared.Models;

namespace Cobra.Server.Mvc
{
    public class MockedSteamAuthMiddleware : ISteamAuthMiddleware
    {
        private readonly Options _options;

        public MockedSteamAuthMiddleware(Options options)
        {
            _options = options;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.User = new ClaimsPrincipal(new CustomIdentity(_options.MockedSteamServiceSteamId));

            return next(context);
        }
    }
}
