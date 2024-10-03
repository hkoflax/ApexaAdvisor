using AdvisorManagement.Api.Abstractions;
using AdvisorManagement.Api.Mappings;
using AdvisorManagement.Api.Models;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AdvisorManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvisorController : ApiBaseController
    {
        [HttpGet("GetAdvisorsList")]
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

        [HttpGet("GetAdvisorById/{advisorId}")]
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

        [HttpPost("CreateAdvisor")]
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

        [HttpPut("UpdateAdvisor")]
        public async Task<ActionResult> UpdateAdvisor([FromBody] UpdateAdvisorModel createAdvisorModel,
            [FromServices] IApiModelMapper<UpdateAdvisorModel, AdvisorDto> mapper,
            [FromServices] IApiModelMapper<AdvisorModel, AdvisorDto> resultMapper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(Commands.UpdateAdvisorRequest(mapper.MapFrom(createAdvisorModel)));

            if (response.Succeeded)
            {
                return Ok(resultMapper.MapTo(response.Data));
            }

            return HandleResponse(response.Context);
        }

        [HttpDelete("DeleteAdvisor/{advisorId}")]
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
