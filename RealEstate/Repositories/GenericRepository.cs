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

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
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

            updatedEntity.CreatedDate = existingEntity.CreatedDate;
            updatedEntity.UpdatedDate = DateTimeOffset.Now;
            List<LeadCommentModel> commentModelList = new List<LeadCommentModel>();

            if (updatedEntity is Lead lead)
            {
                // Check if the updated entity is of type Lead
                if (existingEntity is Lead exLeadEntity)
                {
                    // Clear the existing comments in case of an update
                    //exLeadEntity.LeadCommentModel.Clear();

                    if (lead.LeadCommentModel != null)
                    {
                        foreach (var commentModel in lead.LeadCommentModel)
                        {
                            // Set the timestamp for each comment
                            commentModel.TimeStamp = DateTimeOffset.Now;
                            Console.WriteLine(exLeadEntity);
                            exLeadEntity.LeadCommentModel.Add(commentModel);
                        }
                    }
                }

                // Update other Lead-specific properties if needed
                // exLeadEntity.SomeProperty = lead.SomeProperty;
            }
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
