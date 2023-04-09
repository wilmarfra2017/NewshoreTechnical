using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.ResponseModels
{
    public class TransportResponse
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FlightCarrier { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FlightNumber { get; set; } = string.Empty;
    }
}
