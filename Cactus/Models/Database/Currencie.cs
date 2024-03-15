using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("currencies")]
    public class Currencie
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("country_id")]
        public int CountryId { get; set; }
        [Column("symbol")]
        public string Symbol { get; set; }
    }
}
