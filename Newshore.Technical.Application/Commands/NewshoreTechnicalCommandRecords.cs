using MediatR;
using Newshore.Technical.Transverse.Dto;

namespace Newshore.Technical.Application.Commands
{
    public readonly record struct NewshoreTechnicalCommandRequest(GetJourneysDto journeys) : IRequest<NewshoreTechnicalCommandResponse>;
    public record struct NewshoreTechnicalCommandResponse(List<JourneyDto>? result);
}
