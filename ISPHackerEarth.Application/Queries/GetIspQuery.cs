using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Models.Responses;
using MediatR;

namespace ISPHackerEarth.Application.Queries;

public record GetIspQuery(Guid IspId) : IRequest<ServiceResult<GetISPResponse>>;
