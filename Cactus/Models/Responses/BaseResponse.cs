namespace Cactus.Models.Responses
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T Data {get;set;}
        public string Description { get;set;}
        public int StatusCode { get; set; }
    }
}
