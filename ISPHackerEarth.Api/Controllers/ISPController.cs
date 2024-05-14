using ISPHackerEarth.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ISPHackerEarth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ISPController(IISPService iSPService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllIsp(CancellationToken cancellationToken)
    {
		try
		{
			var response = await iSPService.GetAllISP(cancellationToken);

			if(response.StatusCode != HttpStatusCode.OK)
			{
                return Problem(detail: response.Message, statusCode:(int) response.StatusCode);
            }

			return Ok(response.Data);
		}
		catch (Exception ex)
		{
			return Problem(title: "Server Error", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
		}
    }
}
