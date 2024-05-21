using System.Net;
using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Domain.Common.Services;
using ISPHackerEarth.Domain.Repositories;
using MediatR;

namespace ISPHackerEarth.Application.Commands.Handlers;

public class UpdateIspHandler(ILoggerService logger, IISPRepository iSPRepository) : IRequestHandler<UpdateIspCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateIspCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult();

            logger.LogInformation(message: "Start updating isp details.", ispId: request.ISPRequest.Id, ispName: request.ISPRequest.Name);

            var existingISP = await iSPRepository.GetById(request.ISPRequest.Id, cancellationToken);

            if (existingISP == null)
            {
                logger.LogInformation(message: "Isp not found!.", ispId: request.ISPRequest.Id);
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"ISP with id: {request.ISPRequest.Id} not found in database!";
                return serviceResult;
            }

            // update isp data
            existingISP.Name = request.ISPRequest.Name;
            existingISP.Lowest_Price = request.ISPRequest.Lowest_Price;
            existingISP.Rating = request.ISPRequest.Rating;
            existingISP.Max_Speed = request.ISPRequest.Max_Speed;
            existingISP.Description = request.ISPRequest.Description;
            existingISP.Contact_No = request.ISPRequest.Contact_No;
            existingISP.Email = request.ISPRequest.Email;
            existingISP.Image = request.ISPRequest.Image;
            existingISP.Url = request.ISPRequest.Url;
            existingISP.LastUpdated = DateTime.UtcNow;

            var result = await iSPRepository.Update(existingISP, cancellationToken);

            if (result == false)
            {
                logger.LogError(message: "Updated failed!", ispId: request.ISPRequest.Id, ispName: request.ISPRequest.Name);
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = $"Failed to update ISP details!";
            }

            logger.LogInformation(message: "Susseccfully updated isp details.", ispId: request.ISPRequest.Id, ispName: request.ISPRequest.Name);

            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: request.ISPRequest.Id, ispName: request.ISPRequest.Name, exception: ex);
            throw;
        }
    }
}
