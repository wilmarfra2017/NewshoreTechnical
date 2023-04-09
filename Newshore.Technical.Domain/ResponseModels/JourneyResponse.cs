using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.ResponseModels
{
    public class JourneyResponse
    {
        public int Id { get; set; }

        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        public double Price { get; set; }

        public bool? IsDirectFlight { get; set; }

        public bool? IsRoundTripFlight { get; set; }

        public List<FlightResponse>? Flights { get; set; }
    }
}
