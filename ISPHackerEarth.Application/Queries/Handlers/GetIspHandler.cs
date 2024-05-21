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

public class GetIspHandler(
    ILoggerService logger,
    ICachingService cachingService,
    IISPRepository iSPRepository) : IRequestHandler<GetIspQuery, ServiceResult<GetISPResponse>>
{
    public async Task<ServiceResult<GetISPResponse>> Handle(GetIspQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult<GetISPResponse>();

            logger.LogInformation(message: "Fetching data from cached...", ispId: request.IspId);
            var cachedIsp = cachingService.GetData<GetISPResponse>(CachingKeys.GetIspKey + request.IspId);

            if (cachedIsp != null)
            {
                logger.LogInformation(message: "Successfully fetch data from cached.", ispId: cachedIsp.Id, ispName: cachedIsp.Name);
                serviceResult.Data = cachedIsp;
                return serviceResult;
            }

            logger.LogWarning(message: "No data found in cached", ispId: request.IspId);

            logger.LogInformation(message: "Fetching data from database...", ispId: request.IspId);

            var ispDetails = await iSPRepository.GetById(request.IspId, cancellationToken);

            if (ispDetails == null)
            {
                logger.LogInformation(message: "Isp not found!.", ispId: request.IspId);
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"ISP with id: {request.IspId} not found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Successfully fetch data from database..", ispId: ispDetails.Id, ispName: ispDetails.Name);

            // Mapped isp to response model
            var mappedIsp = ispDetails.ToModel();

            // set data into cached
            cachingService.SetData<GetISPResponse>(CachingKeys.GetIspKey + mappedIsp.Id, mappedIsp);

            serviceResult.Data = mappedIsp;
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: request.IspId, exception: ex);
            throw;
        }
    }
}
