using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class,IAuditable
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.CreatedDate = DateTimeOffset.UtcNow;
            entity.UpdatedDate = DateTimeOffset.UtcNow;
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity updatedEntity)
        {
            var existingEntity = await GetByIdAsync(id);
            if (existingEntity == null)
            {
                return null;
            }

            // Update existingEntity properties with updatedEntity properties

            existingEntity.UpdatedDate = DateTimeOffset.UtcNow;
            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
