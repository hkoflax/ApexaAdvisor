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
    public class AdvisorController: ApiBaseController
    {
        [HttpGet("")]
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

    }
}
