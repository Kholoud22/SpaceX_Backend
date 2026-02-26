namespace SpaceX.Infrastructure
{
    public interface ICacheService
    {
        Task<bool> SaveItemToCache<T>(string itemKey, T value);

        Task<T> GetItemFromCache<T>(string itemKey);
    }
}
