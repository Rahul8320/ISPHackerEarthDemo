using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Mapper;
using ISPHackerEarth.Application.Models.Requests;
using ISPHackerEarth.Application.Models.Responses;
using ISPHackerEarth.Application.Services.Interfaces;
using ISPHackerEarth.Domain.Entities;
using ISPHackerEarth.Domain.Repositories;
using System.Net;

namespace ISPHackerEarth.Application.Services;

internal class ISPService(IISPRepository iSPRepository) : IISPService
{
    public async Task<ServiceResult<IEnumerable<GetISPResponse>>> GetAllISP(CancellationToken cancellationToken)
    {
        var serviceResult = new ServiceResult<IEnumerable<GetISPResponse>>();

        var result = await iSPRepository.GetAll(cancellationToken);

        if (result == null || result.Any() == false)
        {
            serviceResult.StatusCode = HttpStatusCode.NotFound;
            serviceResult.Message = "No ISP data found in database!";
            return serviceResult;
        }

        // sort by last update
        serviceResult.Data = result.OrderByDescending(isp => isp.LastUpdated).Select(isp => isp.ToModel());
        return serviceResult;
    }

    public async Task<ServiceResult<GetISPResponse>> GetISPById(Guid id, CancellationToken cancellationToken)
    {
        var serviceResult = new ServiceResult<GetISPResponse>();

        var result = await iSPRepository.GetById(id, cancellationToken);

        if (result == null)
        {
            serviceResult.StatusCode = HttpStatusCode.NotFound;
            serviceResult.Message = $"ISP with id: {id} not found in database!";
            return serviceResult;
        }

        serviceResult.Data = result.ToModel();
        return serviceResult;
    }

    public async Task<ServiceResult<GetISPResponse>> AddNewISP(CreateISPRequest request, CancellationToken cancellationToken)
    {
        var serviceResult = new ServiceResult<GetISPResponse>();

        // check for existing isp
        var existingISP = await SearchIspByName(request.Name, cancellationToken);

        if (existingISP != null)
        {
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
            serviceResult.StatusCode = HttpStatusCode.BadRequest;
            serviceResult.Message = "Failed to create new ISP!";
            return serviceResult;
        }

        serviceResult.Data = isp.ToModel();
        return serviceResult;
    }

    public async Task<ServiceResult> UpdateISP(UpdateISPRequest request, CancellationToken cancellationToken)
    {
        var serviceResult = new ServiceResult();

        var existingISP = await iSPRepository.GetById(request.Id, cancellationToken);

        if (existingISP == null)
        {
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
            serviceResult.StatusCode = HttpStatusCode.BadRequest;
            serviceResult.Message = $"Failed to update ISP details!";
        }

        return serviceResult;
    }

    public async Task<ServiceResult> DeleteISP(Guid id, CancellationToken cancellationToken)
    {
        var serviceResult = new ServiceResult();

        var existingISP = await iSPRepository.GetById(id, cancellationToken);

        if (existingISP == null)
        {
            serviceResult.StatusCode = HttpStatusCode.NotFound;
            serviceResult.Message = $"ISP with id: {id} not found in database!";
            return serviceResult;
        }

        existingISP.Status = 1;
        existingISP.LastUpdated = DateTime.UtcNow;

        var result = await iSPRepository.Update(existingISP, cancellationToken);

        if (result == false)
        {
            serviceResult.StatusCode = HttpStatusCode.BadRequest;
            serviceResult.Message = $"Failed to delete ISP details!";
        }

        return serviceResult;
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
        var ispList = await iSPRepository.GetAll(cancellationToken);

        if (ispList == null)
        {
            return null;
        }

        var isp = ispList.FirstOrDefault(isp => isp.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return isp;
    }
}
