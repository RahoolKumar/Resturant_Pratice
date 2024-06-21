using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Get restaurants {request.Id}");
            var restaurant = await restaurantRepository.GetByIdAsync(request.Id) 
                ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            //var restaurantDTO = RestaurantDto.FromEntity(restaurant);
            var restaurantDTO = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDTO;
        }
    }

}
