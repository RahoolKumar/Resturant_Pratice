using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantRepository(RestaurantsDbContext restaurantsDbContext) : IRestaurantRepository
    {
        public async Task<int> CreateRestaurant(Restaurant entity)
        {
            restaurantsDbContext.Restaurants.Add(entity);
            await restaurantsDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteRes(Restaurant entity)
        {
            restaurantsDbContext.Remove(entity);
            await restaurantsDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await restaurantsDbContext.Restaurants.ToListAsync();
            return restaurants;
        }
        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? SearchPhrase, int pageSize, 
            int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhrase = SearchPhrase?.ToLower();

            var baseQuery = restaurantsDbContext
            .Restaurants
            .Where(r => SearchPhrase == null || (r.Name.ToLower().Contains(SearchPhrase)
                                                   || r.Description.ToLower().Contains(SearchPhrase)));

            var totalCount = await baseQuery.CountAsync();

            // var totalCount = await baseQuery.CountAsync();

            if(sortBy!=null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name),r=>r.Name},
                    {nameof(Restaurant.Description),r=>r.Description},
                    {nameof(Restaurant.Category),r=>r.Category},
                };

                var selectedColoumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending ?
                baseQuery = baseQuery.OrderBy(selectedColoumn) : baseQuery.OrderByDescending(selectedColoumn);
             }

            var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

            return (restaurants,totalCount);
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
           var restaurant = await restaurantsDbContext.Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefaultAsync(x=>x.Id==id);
            return restaurant;
        }

        public Task SaveChange()
        => restaurantsDbContext.SaveChangesAsync();
        
    }
}
