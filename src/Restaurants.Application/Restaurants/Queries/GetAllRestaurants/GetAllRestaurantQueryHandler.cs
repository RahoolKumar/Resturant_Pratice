using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantQueryHandler(ILogger<GetAllRestaurantQueryHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<GetAllRestaurantQuery, PagedResult<RestaurantDto>>
    {
        public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
        {
          
            logger.LogInformation("Get all restaurants");
            var (restaurants,totalCount) = (await restaurantRepository.GetAllMatchingAsync
                (request.SearchPhrase,request.PageSize,request.PageNumber,request.SoryBy,request.SortDirection));
               
            // var restaurantDTO = restaurants.Select(RestaurantDto.FromEntity);

            var restaurantDTO = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            var result = new PagedResult<RestaurantDto>(restaurantDTO, totalCount, request.PageSize, request.PageNumber);
            
            return result!;
        }
    }
}
