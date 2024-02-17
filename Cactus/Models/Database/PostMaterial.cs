using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("post_material")]
    public class PostMaterial
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        public Post Post {  get; set; }
        [Column("material_type_id")]
        public int MaterialTypeId { get; set; }
        public MaterialType MaterialType { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("path")]
        public string Path { get; set; }
    }
}
