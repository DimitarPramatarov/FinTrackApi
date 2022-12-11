using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FinTrackApi.Infrastructure.Extensions;

namespace FinTrackApi.Infrastructure.Services
{
    
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => this.user = httpContextAccessor.HttpContext?.User;

        public string GetUserName()
            => this.user?.Identity?.Name;

        public string GetId()
            => this.user?.GetId();
    }
}
