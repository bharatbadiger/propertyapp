namespace RealEstate.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RealEstate.Data; // Replace with your DbContext namespace
    using RealEstate.Models;
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


    }
}
