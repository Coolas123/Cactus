namespace Cactus.Models.Database
{
    public class PostComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; }
        public DateTime Created {  get; set; }
    }
}
