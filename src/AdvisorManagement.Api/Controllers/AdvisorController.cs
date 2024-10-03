using AdvisorManagement.Api.Abstractions;
using AdvisorManagement.Api.Mappings;
using AdvisorManagement.Api.Models;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AdvisorManagement.Api.Controllers
{
    /// <summary>
    /// An advisor Controller class
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdvisorController : ApiBaseController
    {
        /// <summary>
        /// Retrieves the list of advisors from the system.
        /// </summary>
        /// <param name="mapper">The mapper service used to map advisor models to DTOs. A <see cref="IApiModelMapper{TApiModel,TDto}"/></param>
        /// <returns> 
        /// A list of advisor DTOs, or an error if something went wrong. A<see cref="List{T}" of <see cref="AdvisorModel"/>/>
        /// </returns>
        /// <response code="200">Returns the list of advisors, A <see cref="StatusCodes.Status200OK"/></response>
        /// <response  code="500">An internal server error occurred,A <see cref="StatusCodes.Status500InternalServerError"/> </response>
        [HttpGet("GetAdvisorsList")]
        [ProducesResponseType(typeof(AdvisorModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAdvisorsList([FromServices] IApiModelMapper<AdvisorModel, AdvisorDto> mapper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(Queries.GetAdvisorList());

            if (response.Succeeded)
            {
                return Ok(mapper.MapTo(response.Data));
            }

            return HandleResponse(response.Context);
        }

        /// <summary>
        /// Retrieve an advisor by Id.
        /// </summary>
        /// <param name="advisorId">The unique Id of the Advisor to retrieve.</param>
        /// <param name="mapper">The mapper service used to map advisor models to DTOs. A <see cref="IApiModelMapper{TApiModel,TDto}"/></param>
        /// <returns>A <see cref="AdvisorModel"/> representing the advisor retrieved by the Id.</returns>
        /// <response code="200">Returns the list of advisors, A <see cref="StatusCodes.Status200OK"/></response>
        /// <response code="404">A no found result , A <see cref="StatusCodes.Status404NotFound"/></response>
        /// <response  code="500">An internal server error occurred,A <see cref="StatusCodes.Status500InternalServerError"/> </response>
        [HttpGet("GetAdvisorById/{advisorId}")]
        [ProducesResponseType(typeof(AdvisorModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAdvisorById(Guid advisorId, [FromServices] IApiModelMapper<AdvisorModel, AdvisorDto> mapper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(Queries.GetAdvisorById(advisorId));

            if (response.Succeeded)
            {
                return Ok(mapper.MapTo(response.Data));
            }

            return HandleResponse(response.Context);
        }

        /// <summary>
        /// Create a new Advisor
        /// </summary>
        /// <param name="createAdvisorModel"> A <see cref="CreateAdvisorModel"/> representing the details to use to create a new Advisor.</param>
        /// <param name="mapper">The mapper service used to map advisor models to DTOs. A <see cref="IApiModelMapper{TApiModel,TDto}"/></param>
        /// <param name="mapper">The mapper service used to map advisor Dtos to models. A <see cref="IApiModelMapper{TDto,TApiModel}"/></param>
        /// <returns>A <see cref="AdvisorModel"/> representing the newly created Advisor.</returns>
        /// <response code="200">Returns the list of advisors, A <see cref="StatusCodes.Status200OK"/></response>
        /// <response code="400">A no found result , A <see cref="StatusCodes.Status400BadRequest"/></response>
        /// <response  code="500">An internal server error occurred,A <see cref="StatusCodes.Status500InternalServerError"/> </response>
        [HttpPost("CreateAdvisor")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<AdvisorModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateAdvisor([FromBody] CreateAdvisorModel createAdvisorModel,
            [FromServices] IApiModelMapper<CreateAdvisorModel, AdvisorDto> mapper,
            [FromServices] IApiModelMapper<AdvisorModel, AdvisorDto> resultMapper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(Commands.CreateAdvisorRequest(mapper.MapFrom(createAdvisorModel)));

            if (response.Succeeded)
            {
                return Ok(resultMapper.MapTo(response.Data));
            }

            return HandleResponse(response.Context);
        }

        /// <summary>
        /// Update an existing Advisor
        /// </summary>
        /// <param name="updateAdvisorModel"> A <see cref="UpdateAdvisorModel"/> representing the details to use to update the existing Advisor.</param>
        /// <param name="mapper">The mapper service used to map advisor models to DTOs. A <see cref="IApiModelMapper{TApiModel,TDto}"/></param>
        /// <param name="mapper">The mapper service used to map advisor Dtos to models. A <see cref="IApiModelMapper{TDto,TApiModel}"/></param>
        /// <returns>A <see cref="AdvisorModel"/> representing the updated Advisor.</returns>
        /// <response code="200">Returns the list of advisors, A <see cref="StatusCodes.Status200OK"/></response>
        /// <response code="400">A no found result , A <see cref="StatusCodes.Status400BadRequest"/></response>
        /// <response  code="500">An internal server error occurred,A <see cref="StatusCodes.Status500InternalServerError"/> </response>
        [HttpPut("UpdateAdvisor")]
        [ProducesResponseType(typeof(AdvisorModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAdvisor([FromBody] UpdateAdvisorModel updateAdvisorModel,
            [FromServices] IApiModelMapper<UpdateAdvisorModel, AdvisorDto> mapper,
            [FromServices] IApiModelMapper<AdvisorModel, AdvisorDto> resultMapper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(Commands.UpdateAdvisorRequest(mapper.MapFrom(updateAdvisorModel)));

            if (response.Succeeded)
            {
                return Ok(resultMapper.MapTo(response.Data));
            }

            return HandleResponse(response.Context);
        }

        /// <summary>
        /// Delete an advisor by Id.
        /// </summary>
        /// <param name="advisorId">The unique Id of the Advisor to Delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Returns the list of advisors, A <see cref="StatusCodes.Status204NoContent"/></response>
        /// <response code="404">A no found result , A <see cref="StatusCodes.Status404NotFound"/></response>
        /// <response  code="500">An internal server error occurred,A <see cref="StatusCodes.Status500InternalServerError"/> </response>
        [HttpDelete("DeleteAdvisor/{advisorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAdvisor(Guid advisorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(Commands.DeleteAdvisorRequest(advisorId));

            if (response.Succeeded)
            {
                return NoContent();
            }

            return HandleResponse(response.Context);
        }
    }
}
