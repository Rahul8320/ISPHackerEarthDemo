using System.Net;
using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Domain.Common.Services;
using ISPHackerEarth.Domain.Repositories;
using MediatR;

namespace ISPHackerEarth.Application.Commands.Handlers;

public class DeleteIspHandler(ILoggerService logger, IISPRepository iSPRepository) : IRequestHandler<DeleteIspCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteIspCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult();

            logger.LogInformation(message: "Fetching data from repository.", ispId: request.IspId);

            var existingISP = await iSPRepository.GetById(request.IspId, cancellationToken);

            if (existingISP == null)
            {
                logger.LogInformation(message: "Isp not found!.", ispId: request.IspId);
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"ISP with id: {request.IspId} not found in database!";
                return serviceResult;
            }

            existingISP.Status = 1;
            existingISP.LastUpdated = DateTime.UtcNow;

            var result = await iSPRepository.Update(existingISP, cancellationToken);

            if (result == false)
            {
                logger.LogError(message: "Delete failed", ispId: request.IspId);
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = $"Failed to delete ISP details!";
            }

            logger.LogInformation(message: "Successfully deleted isp from database.", ispId: request.IspId);

            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: request.IspId, exception: ex);
            throw;
        }
    }
}
