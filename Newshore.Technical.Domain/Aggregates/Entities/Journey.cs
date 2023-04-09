using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newshore.Technical.Domain.Aggregates.Entities
{
    [Table("Journey")]
    public class Journey
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("origin")]
        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [Column("destination")]
        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        [Column("price")]
        public double Price { get; set; }

        [Column("isDirectFlight")]
        public bool? IsDirectFlight { get; set; }

        [Column("isRoundTripFlight")]
        public bool? IsRoundTripFlight { get; set; }
    }
}
