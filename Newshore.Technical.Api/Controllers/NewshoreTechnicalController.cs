using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newshore.Technical.Application.Commands;
using Newshore.Technical.Transverse.Dto;

namespace Newshore.Technical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewshoreTechnicalController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NewshoreTechnicalController> _logger;

        public NewshoreTechnicalController(IMediator mediator, ILogger<NewshoreTechnicalController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("ProcessNewshoreTechnical")]
        public async Task<NewshoreTechnicalCommandResponse> ProcessNewshoreTechnical(NewshoreTechnicalCommandRequest request)
        {
            _logger.Log(LogLevel.Information, $"NewshoreTechnicalController - ProcessNewshoreTechnical - Success - origin: {request.journeys.Origin} destination: {request.journeys.Destination}");
            var response = await _mediator.Send(new NewshoreTechnicalCommandRequest(request.journeys));
            List<JourneyDto>? result = response.result;
            if (result == null || !result.Any())
            {
                throw new Exception("Request could not be processed, since it was not possible to calculate the route");
            }
            return response;
        }

    }
}
