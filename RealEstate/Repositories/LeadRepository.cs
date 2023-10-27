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
                    //.Include(l => l.CreatedBy)
                    //.Include(l => l.LeadCommentModel) // Include the LeadCommentModel
                    .Where(l => l.Id == id)
                    .FirstOrDefaultAsync();
            await _context.Entry(lead).Reference(l => l.CreatedBy).LoadAsync();
            await _context.Entry(lead).Collection(l => l.LeadCommentModel).LoadAsync();

            if (lead != null && lead.LeadCommentModel != null)
            {
                // Sort the comments within LeadCommentModel by timestamp
                lead.LeadCommentModel = lead.LeadCommentModel
                    .OrderByDescending(comment => comment.TimeStamp)
                    .ToList();
            }
            return lead;
        }

        public override async Task<IEnumerable<Lead>> GetAllAsync()
        {
            var leads = await _context.Set<Lead>().ToListAsync();
            foreach (var lead in leads)
            {
                await _context.Entry(lead).Reference(l => l.CreatedBy).LoadAsync();
                if (lead != null && lead.LeadCommentModel != null)
                {
                    // Sort the comments within LeadCommentModel by timestamp
                    lead.LeadCommentModel = lead.LeadCommentModel
                        .OrderByDescending(comment => comment.TimeStamp)
                        .ToList();
                }
                await _context.Entry(lead).Collection(l => l.LeadCommentModel).LoadAsync();
            }

            return leads;

        }


        public async Task<IEnumerable<Lead>> GetAllLeadsByCreatedById(int userId)
        {

            var leads = await _context.Leads
            .Where(lead => lead.CreatedById == userId)
            .ToListAsync();

            foreach (var lead in leads)
            {
                await _context.Entry(lead).Reference(l => l.CreatedBy).LoadAsync();
                if (lead != null && lead.LeadCommentModel != null)
                {
                    // Sort the comments within LeadCommentModel by timestamp
                    lead.LeadCommentModel = lead.LeadCommentModel
                        .OrderByDescending(comment => comment.TimeStamp)
                        .ToList();
                }
                await _context.Entry(lead).Collection(l => l.LeadCommentModel).LoadAsync();
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
