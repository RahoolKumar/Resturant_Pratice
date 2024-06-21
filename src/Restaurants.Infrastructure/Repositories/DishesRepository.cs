using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepository(RestaurantsDbContext restaurantsDbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
           restaurantsDbContext.Dishes.Add(entity);
            await restaurantsDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(IEnumerable<Dish> entities)
        {
            restaurantsDbContext.Dishes.RemoveRange(entities);
            await restaurantsDbContext.SaveChangesAsync();

        }
    }
}
