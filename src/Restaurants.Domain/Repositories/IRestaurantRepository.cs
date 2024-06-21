
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        Task<int> CreateRestaurant(Restaurant entity);
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);

        Task DeleteRes(Restaurant entity);

        Task SaveChange();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? SearchPhrase,int pageSize,int pageNumber,
            string ? Sort, SortDirection sortDirection );

    }
}
