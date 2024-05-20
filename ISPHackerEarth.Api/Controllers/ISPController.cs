using ISPHackerEarth.Application.Models.Requests;
using ISPHackerEarth.Application.Services.Interfaces;
using ISPHackerEarth.Domain.Common.Exceptions;
using ISPHackerEarth.Domain.Common.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ISPHackerEarth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ISPController(IISPService iSPService, ILoggerService logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllIsp(CancellationToken cancellationToken)
    {
        try
        {
            var response = await iSPService.GetAllISP(cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(response.Data);
        }
        catch (ISPException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw new ISPException(HttpStatusCode.InternalServerError, ex);
        }
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetIspDetail(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await iSPService.GetISPById(id, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(response.Data);
        }
        catch (ISPException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: id, exception: ex);
            throw new ISPException(HttpStatusCode.InternalServerError, ex);
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

            var response = await iSPService.AddNewISP(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return CreatedAtAction(nameof(GetIspDetail), new { id = response.Data.Id }, response.Data);
        }
        catch (ISPException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispName: request.Name, exception: ex);
            throw new ISPException(HttpStatusCode.InternalServerError, ex);
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

            var response = await iSPService.UpdateISP(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return NoContent();
        }
        catch (ISPException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: request.Id, ispName: request.Name, exception: ex);
            throw new ISPException(HttpStatusCode.InternalServerError, ex);
        }
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteISP(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await iSPService.DeleteISP(id, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return NoContent();
        }
        catch (ISPException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: id, exception: ex);
            throw new ISPException(HttpStatusCode.InternalServerError, ex);
        }
    }
}
