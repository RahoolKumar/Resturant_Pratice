using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurants.Application.Users.UserContext;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class MinimumAgeRequirmentsHandler(ILogger<MinimumAgeRequirmentsHandler> logger,
        IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirments>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirments requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("User : {Email}, date of birth {DoB} - Handling MinimumAgeRequirment",
                currentUser.Email, currentUser.DateOfBirth);

            if(currentUser.DateOfBirth==null)
            {
                logger.LogInformation("User Date of Birth is null");
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization Succeeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
