using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American"];
        public CreateRestaurantDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required");

            RuleFor(dto => dto.Category).Must(category => validCategories.Contains(category))
                .WithMessage("Invalid Category.Please choose from the valid category");
            //RuleFor(dto => dto.Category).NotEmpty().WithMessage("Category is requird.");
            /* RuleFor(dto => dto.Category)
                 .Custom((value, context) =>
                 {
                     var isValidCategory = validCategories.Contains(value);
                     if(!isValidCategory)
                     {
                         context.AddFailure("Category", "Invalid Category please choose from valid categories"); 
                     }
                 });*/

            RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Please provide a valid email address");


        }
    }
}
