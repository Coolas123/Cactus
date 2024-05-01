using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("tag")]
    public class Tag
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(64)")]
        public string Name { get; set; }
    }
}
