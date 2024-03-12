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
        [Column("description")]
        public string Description { get; set; }
        [Column("option_name")]
        public string OptionName { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("monetization_type_id")]
        public int MonetizationTypeId { get; set; }
    }
}
