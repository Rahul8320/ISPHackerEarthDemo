using ISPHackerEarth.Application.Common;
using MediatR;

namespace ISPHackerEarth.Application.Commands;

public record DeleteIspCommand(Guid IspId) : IRequest<ServiceResult>;