using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidation:AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidation()
        {
            RuleFor(c => c.Name).Length(3, 100);
        }
    }
}
