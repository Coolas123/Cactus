namespace Cactus.Models.Responses
{
    public interface IBaseResponse<T>
    {
        T Data { get; set; }
    }
}
