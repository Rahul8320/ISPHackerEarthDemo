using ISPHackerEarth.Application.Common;
using ISPHackerEarth.Application.Models.Requests;
using MediatR;

namespace ISPHackerEarth.Application.Commands;

public record UpdateIspCommand(UpdateISPRequest ISPRequest) : IRequest<ServiceResult>;