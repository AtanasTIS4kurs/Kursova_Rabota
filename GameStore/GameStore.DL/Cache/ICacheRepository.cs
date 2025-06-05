using GameStore.Models.DTO;

namespace GameStore.DL.Cache
{
    public interface ICacheRepository<TKey, TData> where TData : ICacheItem<TKey>
    {
        Task<IEnumerable<TData?>> FullLoad();

        Task<IEnumerable<TData?>> DifLoad(DateTime lastExecuted);
    }
}
