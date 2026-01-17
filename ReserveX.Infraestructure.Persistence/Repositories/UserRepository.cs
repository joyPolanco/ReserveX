using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Domain.Entities;
using ReserveX.Core.Domain.Interfaces;


namespace ReserveX.Infraestructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly Contexts.AppDbContext _context;

        public UserRepository(Contexts.AppDbContext context) : base(context)
        {
            _context=context;
        }

        public async Task<User?> GetByEmail (string email)
        {
            return await _context.Set<User>().Where(r=>r.Email==email).FirstOrDefaultAsync();

        }

        public async Task<bool> UserIsActive (Guid UserId)
        {
            var user = await _context.Set<User>().Where(r => r.Id == UserId).FirstOrDefaultAsync();
            if (user == null) return false;

            if (user.Status == Core.Domain.Common.enums.Status.INACTIVE) return false;
            return true;
        }

        public  async Task<User?> UpdateUserByGuidAsync(Guid id, User entity)
        {
            var entry = await _context.Set<User>().FindAsync(id);
            if (entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return entry;
        }

        public virtual async Task<List<User>?> GetUserListPaged(int page, int Pagesize)
        {
            return await _context.Set<User>().OrderBy(x => x.CreatedAt).Skip((page - 1) * Pagesize).Take(Pagesize).ToListAsync();
        }
    }
}
