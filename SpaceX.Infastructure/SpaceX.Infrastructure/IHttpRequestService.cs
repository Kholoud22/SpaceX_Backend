namespace SpaceX.Infrastructure
{
    public interface IHttpRequestService
    {
        Task<T> GetResponseInJson<T>(string requestUri);
    }
}