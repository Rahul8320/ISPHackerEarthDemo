using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Mapper;
using ISPHackerEarth.Application.Models.Requests;
using ISPHackerEarth.Application.Models.Responses;
using ISPHackerEarth.Application.Services.Interfaces;
using ISPHackerEarth.Domain.Common.Service;
using ISPHackerEarth.Domain.Entities;
using ISPHackerEarth.Domain.Repositories;
using System.Net;

namespace ISPHackerEarth.Application.Services;

internal class ISPService(IISPRepository iSPRepository, ILoggerService logger) : IISPService
{
    public async Task<ServiceResult<IEnumerable<GetISPResponse>>> GetAllISP(CancellationToken cancellationToken)
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

    public async Task<ServiceResult<GetISPResponse>> GetISPById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult<GetISPResponse>();

            logger.LogInformation(message: "Fetching data from repository.", ispId: id);

            var result = await iSPRepository.GetById(id, cancellationToken);

            if (result == null)
            {
                logger.LogInformation(message: "Isp not found!.", ispId: id);
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"ISP with id: {id} not found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Fatch successfully.", ispId: result.Id, ispName: result.Name);

            serviceResult.Data = result.ToModel();
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: id, exception: ex);
            throw;
        }
    }

    public async Task<ServiceResult<GetISPResponse>> AddNewISP(CreateISPRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult<GetISPResponse>();

            logger.LogInformation(message: "Creating new isp...", ispName: request.Name);

            // check for existing isp
            var existingISP = await SearchIspByName(request.Name, cancellationToken);

            if (existingISP != null)
            {
                logger.LogWarning(message: "Isp with same name already exists in database.", ispName: request.Name);
                serviceResult.StatusCode = HttpStatusCode.Conflict;
                serviceResult.Message = $"An ISP with name: {existingISP.Name} already exists.";
                return serviceResult;
            }

            var isp = new ISP()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Contact_No = request.Contact_No,
                Description = request.Description,
                Lowest_Price = request.Lowest_Price,
                Max_Speed = request.Max_Speed,
                Image = request.Image,
                Url = request.Url,
                Rating = GetRandomRating(),
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
            };

            var result = await iSPRepository.Add(isp, cancellationToken);

            if (result == false)
            {
                logger.LogError(message: "Create failed", ispName: request.Name);
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
            logger.LogError(message: ex.Message, ispName: request.Name, exception: ex);
            throw;
        }
    }

    public async Task<ServiceResult> UpdateISP(UpdateISPRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult();

            logger.LogInformation(message: "Start updating isp details.", ispId: request.Id, ispName: request.Name);

            var existingISP = await iSPRepository.GetById(request.Id, cancellationToken);

            if (existingISP == null)
            {
                logger.LogInformation(message: "Isp not found!.", ispId: request.Id);
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"ISP with id: {request.Id} not found in database!";
                return serviceResult;
            }

            // update isp data
            existingISP.Name = request.Name;
            existingISP.Lowest_Price = request.Lowest_Price;
            existingISP.Rating = request.Rating;
            existingISP.Max_Speed = request.Max_Speed;
            existingISP.Description = request.Description;
            existingISP.Contact_No = request.Contact_No;
            existingISP.Email = request.Email;
            existingISP.Image = request.Image;
            existingISP.Url = request.Url;
            existingISP.LastUpdated = DateTime.UtcNow;

            var result = await iSPRepository.Update(existingISP, cancellationToken);

            if (result == false)
            {
                logger.LogError(message: "Updated failed!", ispId: request.Id, ispName: request.Name);
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = $"Failed to update ISP details!";
            }

            logger.LogInformation(message: "Susseccfully updated isp details.", ispId: request.Id, ispName: request.Name);

            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: request.Id, ispName: request.Name, exception: ex);
            throw;
        }
    }

    public async Task<ServiceResult> DeleteISP(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new ServiceResult();

            logger.LogInformation(message: "Fetching data from repository.", ispId: id);

            var existingISP = await iSPRepository.GetById(id, cancellationToken);

            if (existingISP == null)
            {
                logger.LogInformation(message: "Isp not found!.", ispId: id);
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"ISP with id: {id} not found in database!";
                return serviceResult;
            }

            existingISP.Status = 1;
            existingISP.LastUpdated = DateTime.UtcNow;

            var result = await iSPRepository.Update(existingISP, cancellationToken);

            if (result == false)
            {
                logger.LogError(message: "Delete failed", ispId: id);
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = $"Failed to delete ISP details!";
            }

            logger.LogInformation(message: "Successfully deleted isp from database.", ispId: id);

            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ispId: id, exception: ex);
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
