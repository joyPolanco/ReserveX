using ReserveX.Core.Domain.Entities;

namespace ReserveX.Core.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmail(string email);
        Task<List<User>?> GetUserListPaged(int page, int Pagesize);
        Task<User?> UpdateUserByGuidAsync(Guid id, User entity);
        Task<bool> UserIsActive(Guid UserId);
    }
}
