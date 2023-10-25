using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using System.Collections.Generic;

namespace RealEstate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<User> Users { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lead>()
                .HasOne(lead => lead.CreatedBy) // CreatedBy navigation property
                .WithMany() // No need to specify a navigation property on the other side
                .HasForeignKey(lead => lead.CreatedById); // Foreign key relationship


            modelBuilder.Entity<Property>()
                .HasMany(p => p.Images)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            // Other configurations...
        }


        //public override int SaveChanges()
        //{
        //    var currentTime = DateTimeOffset.UtcNow;

        //    foreach (var entry in ChangeTracker.Entries<IAuditable>())
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedDate = currentTime;
        //        }
        //        if (entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.UpdatedDate = currentTime;
        //        }
        //    }

        //    return base.SaveChanges();
        //}

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    var currentTime = DateTimeOffset.UtcNow;

        //    foreach (var entry in ChangeTracker.Entries<IAuditable>())
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedDate = currentTime;
        //            entry.Entity.UpdatedDate = currentTime;
        //        }

        //        if (entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.UpdatedDate = currentTime;
        //        }
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}

    }
}
