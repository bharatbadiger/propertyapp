namespace RealEstate.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RealEstate.Constants;
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

        public async Task<IEnumerable<Property>> GetPropertiesByCityAsync(string city)
        {
            var propertiesInCity = await _context.Properties
                .Where(p => p.City == city)
                .ToListAsync();

            foreach (var property in propertiesInCity)
            {
                await _context.Entry(property).Collection(p => p.Images).LoadAsync();
            }

            return propertiesInCity;
        }

        public async Task<IEnumerable<Property>> GetPropertiesByCityAndTypeAsync(string city, PropertyType propertyType)
        {
            return await _context.Properties
                .Where(p => p.City == city && p.Type == propertyType)
                .ToListAsync();
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
            var existingProperty = await GetByIdAsync(id);
            if (existingProperty==null)
            {
                return null;
            }

            // Use the Entry method to track the entity
            _context.Entry(existingProperty).State = EntityState.Detached;

            // Loop through the properties and update if they are not null
            foreach (var property in typeof(Property).GetProperties())
            {
                var updatedValue = property.GetValue(updatedProperty);
                if (updatedValue != null)
                {
                    property.SetValue(existingProperty, updatedValue);
                }
            }

            // Update the modified entity in the database
            await _context.SaveChangesAsync();
            return existingProperty;
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
