using Microsoft.AspNetCore.Mvc;
using Splitit.App.Exceptions;
using Splitit.App.Models;
using Splitit.Splitit.Dto;
using Splitit.Splitit.Entities;
using Splitit.Splitit.Services;
using Splitit.Splitit.ValueObjects;
using Splitit.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Splitit.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ActorService _actorService;

        public ActorsController(ActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns a list of actors", typeof(IEnumerable<Actor>))]
        [SwaggerResponseExample(200, typeof(GetAllActorsExample))]
        public ActionResult<IEnumerable<Actor>> GetAllActors([FromQuery] ActorSearchCriteria criteria)
        {
            var actors = _actorService.GetAllActors(criteria);
            return Ok(actors);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns an actor", typeof(DetailedActorDto))]
        [SwaggerResponseExample(200, typeof(GetActorsExample))]
        [SwaggerResponse(404, "Actor not found")]
        [SwaggerResponseExample(404,typeof(NotFoundExample))]
        public ActionResult<Actor> GetActorById(string id)
        {
            var actor = _actorService.GetActorById(id);
            if (actor == null)
            {
                return NotFound();
            }
            return Ok(actor);
        }

        [HttpPost]
        [SwaggerResponse(200, "Actor added successfully", typeof(string))]
        [SwaggerResponse(400, "Bad request - Invalid actor details", typeof(object))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponse(409, "Invalid operation", typeof(object))]
        [SwaggerResponseExample(409, typeof(InvalidOperationResponseExample))]
        public ActionResult AddActor([FromBody] ActorRequest actor)
        {
            try
            {
                string actorId = _actorService.AddActor(new DetailedActorDto(actor.Id, actor.Name, actor.Details, actor.Type, new Rank(actor.Rank), actor.Source));
                return Ok($"Id = {actorId}");
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationAppException(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponseExample(200, typeof(OperationSuccessExample))]
        [SwaggerResponse(409, "Invalid operation", typeof(object))]
        [SwaggerResponseExample(409, typeof(InvalidOperationResponseExample))]
        public ActionResult UpdateActor(string id, [FromBody] ActorRequest actor)
        {
            try
            {
                _actorService.UpdateActor(id, new DetailedActorDto(actor.Id, actor.Name, actor.Details, actor.Type, new Rank(actor.Rank), actor.Source));
                return Ok("Success");
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationAppException(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponseExample(200, typeof(OperationSuccessExample))]
        public ActionResult DeleteActor(string id)
        {
            _actorService.DeleteActor(id);
            return Ok("Success");
        }

    }

}