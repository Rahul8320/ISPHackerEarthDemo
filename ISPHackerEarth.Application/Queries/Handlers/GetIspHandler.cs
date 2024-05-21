using System.Net;
using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Mapper;
using ISPHackerEarth.Application.Models.Responses;
using ISPHackerEarth.Domain.Common.Services;
using ISPHackerEarth.Domain.Repositories;
using MediatR;

namespace ISPHackerEarth.Application.Queries.Handlers;

public class GetIspHandler(ILoggerService logger, IISPRepository iSPRepository) : IRequestHandler<GetIspQuery, ServiceResult<GetISPResponse>>
{
    public async Task<ServiceResult<GetISPResponse>> Handle(GetIspQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult<GetISPResponse>();

            logger.LogInformation(message: "Fetching data from repository.", ispId: request.IspId);

            var result = await iSPRepository.GetById(request.IspId, cancellationToken);

            if (result == null)
            {
                logger.LogInformation(message: "Isp not found!.", ispId: request.IspId);
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"ISP with id: {request.IspId} not found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Fatch successfully.", ispId: result.Id, ispName: result.Name);

            serviceResult.Data = result.ToModel();
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: request.IspId, exception: ex);
            throw;
        }
    }
}
