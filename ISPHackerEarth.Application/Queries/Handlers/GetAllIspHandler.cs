using System.Net;
using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Mapper;
using ISPHackerEarth.Application.Models.Responses;
using ISPHackerEarth.Application.Services;
using ISPHackerEarth.Application.Services.Interfaces;
using ISPHackerEarth.Domain.Common.Services;
using ISPHackerEarth.Domain.Repositories;
using MediatR;

namespace ISPHackerEarth.Application.Queries.Handlers;

public class GetAllIspHandler(
    ILoggerService logger,
    ICachingService cachingService,
    IISPRepository iSPRepository) : IRequestHandler<GetAllIspQuery, ServiceResult<IEnumerable<GetISPResponse>>>
{
    public async Task<ServiceResult<IEnumerable<GetISPResponse>>> Handle(GetAllIspQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult<IEnumerable<GetISPResponse>>();

            logger.LogInformation(message: "Fetching data from cached...");
            var cachedIsps = cachingService.GetData<IEnumerable<GetISPResponse>>(CachingKeys.GetAllIspKey);

            if (cachedIsps != null && cachedIsps.Any())
            {
                logger.LogInformation(message: "Successfully fetch data from cached.");
                serviceResult.Data = cachedIsps;
                return serviceResult;
            }

            logger.LogWarning(message: "No data found in cached");

            logger.LogInformation(message: "Fetching data from database...");
            var isps = await iSPRepository.GetAll(cancellationToken);

            if (isps == null || isps.Any() == false)
            {
                logger.LogWarning(message: "No data found in database.");
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = "No ISP data found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Successfully fetch data from database.");

            // Mapped isp to response model
            var mappedIsps = isps.OrderByDescending(isp => isp.LastUpdated).Select(isp => isp.ToModel());

            // set data into cached
            cachingService.SetData<IEnumerable<GetISPResponse>>(CachingKeys.GetAllIspKey, mappedIsps);

            // sort by last update
            serviceResult.Data = mappedIsps;
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }
}
