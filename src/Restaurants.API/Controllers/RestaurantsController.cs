using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]

    public class RestaurantsController(IMediator mediator) :ControllerBase
    {
         [AllowAnonymous]
        [HttpGet]
        //[Authorize(Policy = PolicyName.CreatedAtLeast2Restaurant)]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantQuery query) {
           // var restaurant = await mediator.Send(query));
            var restaurants = await mediator.Send(query);
            return Ok(restaurants);
            //return Ok(restaurant);

        }
        [HttpGet("{id}")]
       // [Authorize(Policy = PolicyName.HasNationality)]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
        {
            try
            {
                //var restaurant = await restaurantService.GetById(id);
                var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
               
                return Ok(restaurant);

            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong");

            }
          
        }

        [HttpPost]
        [Authorize(Roles =UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            /*if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
           // int id = await restaurantService.CreateRes(createRestaurantDTo);
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute]int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
           
            return NotFound();
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id,UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command)    ;

            return NoContent();
        }

        [HttpPost("{id}/logo")]
        public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
        {
            using var stream = file.OpenReadStream() ;
            var command = new UploadRestaurantLogoCommand()
            {
                RestaurantId = id,
                FileName = file.FileName,
                File = stream
            
            };
            await mediator.Send(command) ;
            return NoContent();

        }


    }
}
