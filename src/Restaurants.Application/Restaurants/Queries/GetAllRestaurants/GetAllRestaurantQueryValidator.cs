using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantQueryValidator : AbstractValidator<GetAllRestaurantQuery>
    {
        private int[] allowPageSizes = [5, 10, 15, 30];
        private string[] allowSortByColoumnNames = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Category),
        nameof(RestaurantDto.Description)];
        public GetAllRestaurantQueryValidator()
        {
            RuleFor(r=>r.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize).Must(value=>allowPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",",allowPageSizes)}]");

            RuleFor(r => r.SoryBy)
                .Must(values => allowSortByColoumnNames.Contains(values))
                .When(q=>q.SoryBy!=null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowSortByColoumnNames)}]");
        }
    }
}
