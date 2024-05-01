using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("post")]
    public class Post
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId {  get; set; }
        public Author User {  get; set; }
        [Column("title", TypeName = "varchar(64)")]
        public string Title { get; set; }
        [Column("description", TypeName = "varchar(20480)")]
        public string Description { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
    }
}
