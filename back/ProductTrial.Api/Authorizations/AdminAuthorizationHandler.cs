using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProductTrial.Api.Authorizations
{
    public class AdminAuthorizationHandler : AuthorizationHandler<AdminUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminUserRequirement user)
        {
            var email = context.User.FindFirst(ClaimTypes.Email)?.Value;

            if (email == user.Email)
            {
                context.Succeed(user);
            }

            return Task.CompletedTask;
        }
    }

    public class AdminUserRequirement : IAuthorizationRequirement
    {
        public AdminUserRequirement(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}