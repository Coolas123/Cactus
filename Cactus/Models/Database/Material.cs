namespace Cactus.Models.Database
{
    public class Material
    {
        public int Id {  get; set; }
        public int UserId {  get; set; }
        public int MaterialTypeId {  get; set; }
        public MaterialType MaterialType {  get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
    }
}
