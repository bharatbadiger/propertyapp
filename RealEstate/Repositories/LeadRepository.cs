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
            _context = context;
        }

        public override async Task<Lead> GetByIdAsync(int id)
        {
            var lead = await _context.Leads
                .Include(l => l.CreatedBy)
                .FirstOrDefaultAsync(l => l.Id == id);

            return lead;
        }


        public async Task<IEnumerable<Lead>> GetAllLeadsByCreatedById(int userId)
        {

            var leads = await _context.Leads
            .Where(lead => lead.CreatedById == userId)
            .ToListAsync();

            foreach (var lead in leads)
            {
                await _context.Entry(lead).Reference(l => l.CreatedBy).LoadAsync();
                // Load any other related data as needed
            }

            return leads;


            //// Filter leads by the CreatedById property
            //return await _context.Leads
            //    .Where(lead => lead.CreatedById == userId)
            //    .ToListAsync();
        }


    }
}
