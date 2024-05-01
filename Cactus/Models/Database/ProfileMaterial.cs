using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("profile_material")]
    public class ProfileMaterial
    {
        [Column("id")]
        public int Id {  get; set; }
        [Column("user_id")]
        public int UserId {  get; set; }
        public User User { get; set; }
        [Column("material_type_id")]
        public int MaterialTypeId {  get; set; }
        public MaterialType MaterialType {  get; set; }
        [Column("title", TypeName = "varchar(256)")]
        public string Title { get; set; }
        [Column("path")]
        public string Path { get; set; }
    }
}
