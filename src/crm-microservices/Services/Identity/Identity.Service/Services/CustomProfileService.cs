using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Identity.Service.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.Service.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(CustomClaimTypes.Email, user?.Email??string.Empty),
                    new Claim(CustomClaimTypes.UserId, user?.Id?.ToString()??string.Empty),
                    new Claim(CustomClaimTypes.Username, user?.UserName?.ToString()??string.Empty),
                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Add the claims to the context
                context.AddRequestedClaims(claims);
                context.IssuedClaims.AddRange(claims);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }

}

