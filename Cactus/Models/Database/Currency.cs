using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("currencies")]
    public class Currency
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("country_id")]
        public int CountryId { get; set; }
        [Column("symbol", TypeName = "varchar(8)")]
        public string Symbol { get; set; }
    }
}
