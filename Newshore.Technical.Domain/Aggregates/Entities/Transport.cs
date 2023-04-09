using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newshore.Technical.Domain.Aggregates.Entities
{
    [Table("Transport")]
    public class Transport
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("flightCarrier")]
        [MaxLength(100)]
        public string FlightCarrier { get; set; } = string.Empty;

        [Column("flightNumber")]
        [MaxLength(50)]
        public string FlightNumber { get; set; } = string.Empty;
    }
}
