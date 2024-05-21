using System.Net;
using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Mapper;
using ISPHackerEarth.Application.Models.Responses;
using ISPHackerEarth.Domain.Common.Services;
using ISPHackerEarth.Domain.Entities;
using ISPHackerEarth.Domain.Repositories;
using MediatR;

namespace ISPHackerEarth.Application.Commands.Handlers;

public class CreateIspHandler(ILoggerService logger, IISPRepository iSPRepository) : IRequestHandler<CreateIspCommand, ServiceResult<GetISPResponse>>
{
    public async Task<ServiceResult<GetISPResponse>> Handle(CreateIspCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult<GetISPResponse>();

            logger.LogInformation(message: "Creating new isp...", ispName: request.CreateRequest.Name);

            // check for existing isp
            var existingISP = await SearchIspByName(request.CreateRequest.Name, cancellationToken);

            if (existingISP != null)
            {
                logger.LogWarning(message: "Isp with same name already exists in database.", ispName: request.CreateRequest.Name);
                serviceResult.StatusCode = HttpStatusCode.Conflict;
                serviceResult.Message = $"An ISP with name: {existingISP.Name} already exists.";
                return serviceResult;
            }

            var isp = new ISP()
            {
                Id = Guid.NewGuid(),
                Name = request.CreateRequest.Name,
                Email = request.CreateRequest.Email,
                Contact_No = request.CreateRequest.Contact_No,
                Description = request.CreateRequest.Description,
                Lowest_Price = request.CreateRequest.Lowest_Price,
                Max_Speed = request.CreateRequest.Max_Speed,
                Image = request.CreateRequest.Image,
                Url = request.CreateRequest.Url,
                Rating = GetRandomRating(),
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
            };

            var result = await iSPRepository.Add(isp, cancellationToken);

            if (result == false)
            {
                logger.LogError(message: "Create failed", ispName: request.CreateRequest.Name);
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = "Failed to create new ISP!";
                return serviceResult;
            }

            logger.LogInformation(message: "Susseccfully created new isp.", ispId: isp.Id, ispName: isp.Name);

            serviceResult.Data = isp.ToModel();
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispName: request.CreateRequest.Name, exception: ex);
            throw;
        }
    }

    private static double GetRandomRating()
    {
        Random random = new();
        double minValue = 3.0;
        double maxValue = 5.0;
        double randomRating = minValue + random.NextDouble() * (maxValue - minValue);
        return randomRating;
    }

    private async Task<ISP?> SearchIspByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation(message: "Fetching data from repository.", ispName: name);
            var ispList = await iSPRepository.GetAll(cancellationToken);

            if (ispList == null)
            {
                return null;
            }

            var isp = ispList.FirstOrDefault(isp => isp.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return isp;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispName: name, exception: ex);
            throw;
        }
    }
}
