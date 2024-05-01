using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("sub_level_material")]
    public class SubLevelMaterial
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("donation_option_id")]
        public int DonationOptionId { get; set; }
        public DonationOption DonationOption { get; set; }
        [Column("material_type_id")]
        public int MaterialTypeId { get; set; }
        public MaterialType MaterialType { get; set; }
        [Column("title", TypeName = "varchar(64)")]
        public string Title { get; set; }
        [Column("path")]
        public string Path { get; set; }
    }
}
