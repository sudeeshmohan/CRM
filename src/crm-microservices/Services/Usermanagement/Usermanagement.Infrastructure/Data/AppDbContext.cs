using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Usermanagement.Domain.Entities.UserDetails;

namespace Usermanagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var entries = ChangeTracker
        //        .Entries<BaseEntity>();

        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedDate = DateTime.UtcNow;
        //            entry.Entity.UpdatedDate = DateTime.UtcNow;
        //            entry.Entity.IsDeleted = false;
        //        }

        //        if (entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.UpdatedDate = DateTime.UtcNow;
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}
    }
}

