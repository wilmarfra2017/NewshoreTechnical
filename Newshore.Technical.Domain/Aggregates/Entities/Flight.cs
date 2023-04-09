using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newshore.Technical.Domain.Aggregates.Entities
{
    [Table("Flight")]
    public class Flight
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

        [Column("transportId")]
        public int TransportId { get; set; }

        public Transport Transport { get; set; } = new Transport();
    }
}
