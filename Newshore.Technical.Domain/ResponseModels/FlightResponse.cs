using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.ResponseModels
{
    public class FlightResponse
    {
        public int Id { get; set; }

        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        public double Price { get; set; }

        public int TransportId { get; set; }

        public TransportResponse? Transport { get; set; }
    }
}
