using Microsoft.AspNetCore.Authorization;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurants.Application.Users.UserContext;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    internal class CreatedMultipleRestaurantRequirmentHandler(IRestaurantRepository restaurantRepository,
        IUserContext userContext) : AuthorizationHandler<CreatedMultipleRestaurantRequirment>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantRequirment requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            var restaurent = await restaurantRepository.GetAllAsync();

            var userRestaurantCreated = restaurent.Count(r=>r.OwnerId==currentUser!.Id);

            if(userRestaurantCreated>=requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
                    
            }
            else
            {
                context.Fail();

            }
        }
    }
}
