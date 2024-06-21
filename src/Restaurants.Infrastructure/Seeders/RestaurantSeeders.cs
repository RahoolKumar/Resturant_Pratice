using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeders(RestaurantsDbContext restaurantsDbContext) : IRestaurantSeeders
    {
        public async Task Seed()
        {
            if(restaurantsDbContext.Database.GetPendingMigrations().Any())
            {
                await restaurantsDbContext.Database.MigrateAsync();
            }

            if (await restaurantsDbContext.Database.CanConnectAsync())
            {
                if (!restaurantsDbContext.Restaurants.Any())
                {
                    var restaurant = GetRestaurant();
                    restaurantsDbContext.Restaurants.AddRange(restaurant);
                    await restaurantsDbContext.SaveChangesAsync();
                }

                if(!restaurantsDbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    restaurantsDbContext.Roles.AddRange(roles);
                    await restaurantsDbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    /*new (UserRoles.User),
                    new (UserRoles.Owner),
                    new (UserRoles.Admin),*/
                    new(UserRoles.User)
                    {
                        NormalizedName = UserRoles.User.ToUpper()
                    },
                    new(UserRoles.Owner)
                    {
                        NormalizedName = UserRoles.Owner.ToUpper()
                    },
                    new(UserRoles.Admin)
                    {
                        NormalizedName = UserRoles.Admin.ToUpper()
                    }
                ];

            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurant()
        {
            User owner = new User()
            {
                Email = "seed-user@test.com"

            };


            List<Restaurant> restaurants = [
                new()
                {
                    Ower = owner,
                    Name = "KFC",
                    Category = "Fast Food",
                    Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes =
                [
                    new()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                    Address = new()
                    {
                        City = "London",
                        Street = "Cork St 5",
                        PostalCode = "WC2N 5DU"
                    }
                },
                new()
                {
                    Ower = owner,
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "London",
                        Street = "Boots 193",
                        PostalCode = "W1F 8SR"
                    }
                }

            ];
            return restaurants;
        }
    }
}
