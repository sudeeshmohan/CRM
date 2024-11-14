using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Usermanagement.Domain.Abstraction;

namespace Usermanagement.Infrastructure.Data.Interceptor
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData
            , InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context == null) { return; }
            foreach (var item in context.ChangeTracker.Entries<IEntity>())
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreatedDate = DateTime.UtcNow;
                    item.Entity.CreatedBy = "Username";
                }
                if (item.State == EntityState.Added || item.State == EntityState.Modified || item.HasChangedOwnEntities())
                {
                    item.Entity.UpdatedDate = DateTime.UtcNow;
                    item.Entity.UpdatedBy = "Username";
                }
                if (item.State == EntityState.Modified)
                {
                    if (item.Entity.IsDeleted)
                    {
                        item.Entity.DeletedDate = DateTime.UtcNow;
                        item.Entity.DeletedBy = "Username";
                    }
                }
            }
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData
            , InterceptionResult<int> result
            , CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
    public static class Extensions
    {
        public static bool HasChangedOwnEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified)
            );
    }
}
