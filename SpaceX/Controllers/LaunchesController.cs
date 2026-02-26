using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using SpaceX.API;
using SpaceX.Application.Launches.Queries;
using SpaceX.Infrastructure.Launches.Dtos;
using SpaceX.Application.Enums;

namespace SpaceX.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class LaunchesController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        /// <summary>
        /// Get Launches
        /// </summary>
        /// <param name="period">past or upcoming</param>
        /// <returns>List<LaunchResponseDto></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(APIRoutes.Launches.GetAll)]
        public async Task<ActionResult<List<LaunchResponseDto>>> GetAll([FromRoute]LaunchPeriods period)
        {
            var query = new GetLaunchesQuery(period);
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Get Launch by Id
        /// </summary>
        /// <param name="id">launch id</param>
        /// <returns>LaunchResponseDto</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(APIRoutes.Launches.Get)]
        public async Task<ActionResult<LaunchResponseDto>> Get([FromRoute] string id)
        {
            var query = new GetLaunchQuery(id);
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}