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


        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.CreatedDate = DateTimeOffset.Now;
            entity.UpdatedDate = DateTimeOffset.Now;
            if (entity is Lead LeadEntity) {
                foreach (var commentModel in LeadEntity.LeadCommentModel)
                {
                    commentModel.TimeStamp = DateTimeOffset.Now;
                }
            }

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(int id, TEntity updatedEntity)
        {
            var existingEntity = await GetByIdAsync(id);
            if (existingEntity == null)
            {
                return null;
            }

            // Update existingEntity properties with updatedEntity properties

            existingEntity.UpdatedDate = DateTimeOffset.Now;
            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
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
