using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantService(IMapper mapper, IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger) : IRestaurantService
    {
        public async Task<int>? CreateRes(CreateRestaurantDTo createRestaurantDTo)
        {
            logger.LogInformation("Create Restaurant");
            var res = mapper.Map<Restaurant>(createRestaurantDTo);
            var id = await restaurantRepository.CreateRestaurant(res);
            return id;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Get all restaurants");
            var restaurants = await restaurantRepository.GetAllAsync();
            // var restaurantDTO = restaurants.Select(RestaurantDto.FromEntity);

            var restaurantDTO = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantDTO!;
        }

        public async Task<RestaurantDto?> GetById(int id)
        {
            logger.LogInformation($"Get restaurants {id}");
            var restaurant = await restaurantRepository.GetByIdAsync(id);
            //var restaurantDTO = RestaurantDto.FromEntity(restaurant);
            var restaurantDTO = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDTO;
        }
    }
}
