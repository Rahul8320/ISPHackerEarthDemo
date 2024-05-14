using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Mapper;
using ISPHackerEarth.Application.Models.Responses;
using ISPHackerEarth.Application.Services.Interfaces;
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
}
