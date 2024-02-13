namespace Cactus.Models.ViewModels
{
    public class CommentViewModel
    {
        public string Comment {  get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
    }
}
