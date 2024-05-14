using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Models.Responses;

namespace ISPHackerEarth.Application.Services.Interfaces;

public interface IISPService
{
    Task<ServiceResult<IEnumerable<GetISPResponse>>> GetAllISP(CancellationToken cancellationToken);
}
