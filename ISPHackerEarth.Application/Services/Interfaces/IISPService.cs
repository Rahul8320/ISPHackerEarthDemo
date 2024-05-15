using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Models.Requests;
using ISPHackerEarth.Application.Models.Responses;

namespace ISPHackerEarth.Application.Services.Interfaces;

public interface IISPService
{
    Task<ServiceResult<IEnumerable<GetISPResponse>>> GetAllISP(CancellationToken cancellationToken);
    Task<ServiceResult<GetISPResponse>> GetISPById(Guid id, CancellationToken cancellationToken);
    Task<ServiceResult<GetISPResponse>> AddNewISP(CreateISPRequest request, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateISP(UpdateISPRequest request, CancellationToken cancellationToken);
    Task<ServiceResult> DeleteISP(Guid id, CancellationToken cancellationToken);
}
