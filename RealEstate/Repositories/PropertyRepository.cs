namespace RealEstate.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RealEstate.Data; // Replace with your DbContext namespace
    using RealEstate.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PropertyRepository : IRepository<Property>
    {
        private readonly ApplicationDbContext _context; // Replace with your DbContext

        public PropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            await _context.Entry(property).Collection(p => p.Images).LoadAsync();
            return property;
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            var properties = await _context.Properties.ToListAsync();

            foreach (var property in properties)
            {
                // Explicitly load the Images collection for each Property
                await _context.Entry(property).Collection(p => p.Images).LoadAsync();
            }

            return properties;
        }

        public async Task<Property> CreateAsync(Property property)
        {
            //property.Id = Guid.NewGuid();
            property.CreatedDate = DateTimeOffset.UtcNow;
            property.UpdatedDate = DateTimeOffset.UtcNow;
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<Property> UpdateAsync(int id, Property updatedProperty)
        {
            if (!_context.Properties.Any(p => p.Id == id))
            {
                return null;
            }

            updatedProperty.Id = id;
            updatedProperty.UpdatedDate = DateTimeOffset.UtcNow;
            _context.Entry(updatedProperty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedProperty;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return false;
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
