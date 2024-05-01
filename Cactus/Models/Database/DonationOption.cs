using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("donation_option")]
    public class DonationOption
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("author_id")]
        public int AuthorId { get; set; }
        [Column("description", TypeName = "varchar(512)")]
        public string Description { get; set; }
        [Column("option_name", TypeName = "varchar(64)")]
        public string OptionName { get; set; }
        [Column("price", TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [Column("monetization_type_id")]
        public int MonetizationTypeId { get; set; }
    }
}
