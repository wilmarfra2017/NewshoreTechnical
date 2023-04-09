namespace Newshore.Technical.Transverse.Dto
{
    public class JourneyDto
    {
        public int Id { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }
        public bool? IsDirectFlight { get; set; }
        public bool? IsRoundTripFlight { get; set; }
        public List<FlightDto> Flights { get; set; } = new List<FlightDto>();
    }
}
