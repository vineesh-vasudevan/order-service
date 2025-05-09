
namespace OrderService.Infrastructure.Data.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            ApplyAudit(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            ApplyAudit(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        private static void ApplyAudit(DbContext? context)
        {
            if (context == null) return;

            var currentUser = GetCurrentUser();

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                bool isNewlyAdded = entry.State == EntityState.Added;

                if (isNewlyAdded ||
                    entry.State == EntityState.Modified ||
                    entry.HasChangedOwnedEntities())
                {
                    entry.Entity.SetAudit(currentUser, isNewlyAdded);
                }
            }
        }

        private static string GetCurrentUser()
        {
            return "System"; // TODO: Replace with IHttpContextAccessor or another user provider
        }
    }
}