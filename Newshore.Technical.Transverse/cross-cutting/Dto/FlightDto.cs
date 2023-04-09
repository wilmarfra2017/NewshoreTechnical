namespace Newshore.Technical.Transverse.Dto
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }
        public TransportDto Transport { get; set; } = new TransportDto();
    }
}
