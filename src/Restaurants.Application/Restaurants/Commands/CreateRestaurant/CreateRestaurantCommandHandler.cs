using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurants.Application.Users.UserContext;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository,
        IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser =  userContext.GetCurrentUser();


            logger.LogInformation("{UserEmail} [{UserID}] is creating Restaurant {@Restaurant}",
                currentUser.Email,
                currentUser.Id,
                request);
            var res = mapper.Map<Restaurant>(request);
            res.OwnerId = currentUser.Id;
            var id = await restaurantRepository.CreateRestaurant(res);
            return id;
        }
    }
}
