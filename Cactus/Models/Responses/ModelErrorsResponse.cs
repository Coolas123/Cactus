namespace Cactus.Models.Responses
{
    public class ModelErrorsResponse<T> : IBaseResponse<T>
    {
        public T Data { get; set; }
        public Dictionary<string, string> Descriptions { get; set; }
        public int StatusCode { get; set; }
    }
}
