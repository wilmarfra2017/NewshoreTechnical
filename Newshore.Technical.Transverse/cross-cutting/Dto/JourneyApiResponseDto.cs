using System.Runtime.Serialization;

namespace Newshore.Technical.Transverse.Dto
{
    [DataContract]
    public class JourneyApiResponseDto
    {
        [DataMember(Name = "departureStation")]
        public string Origin { get; set; } = string.Empty;

        [DataMember(Name = "arrivalStation")]
        public string Destination { get; set; } = string.Empty;

        [DataMember(Name = "flightCarrier")]
        public string FlightCarrier { get; set; } = string.Empty;

        [DataMember(Name = "flightNumber")]
        public string FlightNumber { get; set; } = string.Empty;

        [DataMember(Name = "price")]
        public double Price { get; set; }
    }
}
