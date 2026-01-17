
using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Infraestructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Contexts.AppDbContext _context;
        public GenericRepository(Contexts.AppDbContext context)
        {
            _context = context;
        }
        public async Task<T?> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result != null ? entity : null;
        }

        public async Task<T?> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T?> UpdateAsync(int id,T entity)
        {
            var entry = await _context.Set<T>().FindAsync(id);
            if (entry != null) 
            { 
                _context.Entry(entry).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return entry;
        }
        public virtual IQueryable<T> GetAllQuery()
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual async Task<List<T>?> GetAllList()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public virtual async Task<List<T>?> GetAllListWithInclude(List<string>properties)
        {

            var query = _context.Set<T>();

            foreach(var property in properties)
            {
                query.Include(property);
            }
            return await query.ToListAsync();
        }

    }
}
