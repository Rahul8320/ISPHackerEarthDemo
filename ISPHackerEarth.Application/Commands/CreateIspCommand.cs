using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Models.Requests;
using ISPHackerEarth.Application.Models.Responses;
using MediatR;

namespace ISPHackerEarth.Application.Commands;

public record CreateIspCommand(CreateISPRequest CreateRequest) : IRequest<ServiceResult<GetISPResponse>>;

