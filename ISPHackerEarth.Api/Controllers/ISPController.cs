using ISPHackerEarth.Application.Commands;
using ISPHackerEarth.Application.Models.Requests;
using ISPHackerEarth.Application.Queries;
using ISPHackerEarth.Domain.Common.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ISPHackerEarth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ISPController(ILoggerService logger, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllIsp(CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllIspQuery();
            var response = await mediator.Send(query, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetIspDetail(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetIspQuery(id);
            var response = await mediator.Send(query, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: id, exception: ex);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateIsp([FromBody] CreateISPRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateIspCommand(request);
            var response = await mediator.Send(command, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return CreatedAtAction(nameof(GetIspDetail), new { id = response.Data.Id }, response.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispName: request.Name, exception: ex);
            throw;
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateISP([FromBody] UpdateISPRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateIspCommand(request);
            var response = await mediator.Send(command, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: request.Id, ispName: request.Name, exception: ex);
            throw;
        }
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteISP(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteIspCommand(id);
            var response = await mediator.Send(command, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: id, exception: ex);
            throw;
        }
    }
}
