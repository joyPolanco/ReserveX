

namespace ReserveX.Core.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task <T?>AddAsync (T entity);
        Task<List<T>?> GetAllList();
        Task<List<T>?> GetAllListWithInclude(List<string> properties);
        IQueryable<T> GetAllQuery();
        public Task<T?> GetById (int id);
        Task <T?>UpdateAsync(int id, T entity);
    }
}
