using System.Net;
using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Mapper;
using ISPHackerEarth.Application.Models.Responses;
using ISPHackerEarth.Domain.Common.Services;
using ISPHackerEarth.Domain.Repositories;
using MediatR;

namespace ISPHackerEarth.Application.Queries.Handlers;

public class GetAllIspHandler(
    ILoggerService logger,
    IISPRepository iSPRepository) : IRequestHandler<GetAllIspQuery, ServiceResult<IEnumerable<GetISPResponse>>>
{
    public async Task<ServiceResult<IEnumerable<GetISPResponse>>> Handle(GetAllIspQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult<IEnumerable<GetISPResponse>>();

            logger.LogInformation(message: "Fetching data from repository.");

            var result = await iSPRepository.GetAll(cancellationToken);

            if (result == null || result.Any() == false)
            {
                logger.LogInformation(message: "No data found.");
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = "No ISP data found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Fatch successfully.");

            // sort by last update
            serviceResult.Data = result.OrderByDescending(isp => isp.LastUpdated).Select(isp => isp.ToModel());
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }
}
