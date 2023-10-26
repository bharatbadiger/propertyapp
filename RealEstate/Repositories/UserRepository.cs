namespace RealEstate.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RealEstate.Data; // Replace with your DbContext namespace
    using RealEstate.Models;
    using RealEstate.Models.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserRepository : GenericRepository<User>
    {
        private readonly ApplicationDbContext _context; // Replace with your DbContext

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByMobileNoAsync(string mobileNo)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.MobileNo == mobileNo);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public List<Property> GetUserFavoriteProperties(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                var favoritePropertyIds = user.FavoritePropertyIds?.ToList();
                var favoriteProperties = _context.Properties
                    .Where(p => favoritePropertyIds.Contains(p.Id))
                    .ToList();
                return favoriteProperties;
            }
            return new List<Property>();
        }


        public async Task<UserWithFavoriteProperties> GetUserWithFavoritePropertiesAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserWithFavoriteProperties
                {
                    Id = u.Id,
                    UserName = u.FullName,
                    FavoriteProperties = u.FavoritePropertyIds.ToList()
                })
                .FirstOrDefaultAsync();
        }



    }
}
