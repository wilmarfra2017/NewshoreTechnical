using MediatR;
using Microsoft.Extensions.Logging;
using Newshore.Technical.Domain.Interfaces;

namespace Newshore.Technical.Application.Commands
{
    public class NewshoreTechnicalCommandHandler : IRequestHandler<NewshoreTechnicalCommandRequest, NewshoreTechnicalCommandResponse>
    {
        private readonly IJourneyDomain _journeyManagerDomain;
        private readonly ILogger<NewshoreTechnicalCommandHandler> _logger;

        public NewshoreTechnicalCommandHandler(IJourneyDomain journeyManagerDomain, ILogger<NewshoreTechnicalCommandHandler> logger)
        {
            _journeyManagerDomain = journeyManagerDomain;
            _logger = logger;
        }

        public async Task<NewshoreTechnicalCommandResponse> Handle(NewshoreTechnicalCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, $"NewshoreTechnicalCommandHandler - Handle - origin: {request.journeys.Origin} destination: {request.journeys.Destination}");
            var result =  await _journeyManagerDomain.FindItinerariesFromOriginToDestination(request.journeys.Origin, request.journeys.Destination);
            return new NewshoreTechnicalCommandResponse(result);
        }
    }
}
