namespace RealEstate.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RealEstate.Data; // Replace with your DbContext namespace
    using RealEstate.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LeadRepository : GenericRepository<Lead>
    {
        private readonly ApplicationDbContext _context; // Replace with your DbContext
        public LeadRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IEnumerable<Lead> GetAllLeadsByCreatedById(int userId)
        {
            // Filter leads by the CreatedById property
            return _context.Leads
                .Where(lead => lead.CreatedById == userId)
                .ToList();
        }


    }
}
