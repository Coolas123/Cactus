namespace Cactus.Models.Database
{
    public class PostMaterial
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post {  get; set; }
        public int MaterialTypeId { get; set; }
        public MaterialType MaterialType { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
    }
}
