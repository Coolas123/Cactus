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
        [Column("title")]
        public string? Title { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
    }
}
