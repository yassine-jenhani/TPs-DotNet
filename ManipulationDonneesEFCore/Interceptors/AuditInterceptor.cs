using manipulationDonneesEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace manipulationDonneesEFCore.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override int SavedChanges(
            SaveChangesCompletedEventData eventData, int result)
        {
            var context = eventData.Context;
            if (context == null) return result;

            // Récupérer les entités modifiées AVANT le save
            var auditLogs = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .Where(e => e.Entity is not AuditLog) // ← éviter boucle infinie
                .Select(e => new AuditLog
                {
                    TableName = e.Entity.GetType().Name,
                    Action = e.State.ToString(),
                    EntityKey = e.Properties
                        .FirstOrDefault(p => p.Metadata.IsPrimaryKey())
                        ?.CurrentValue?.ToString(),
                    Changes = System.Text.Json.JsonSerializer
                        .Serialize(e.CurrentValues.ToObject()),
                    Date = DateTime.UtcNow
                }).ToList();

            if (auditLogs.Any())
            {
                // Utiliser SaveChangesWithoutInterceptors pour éviter la boucle
                context.Set<AuditLog>().AddRange(auditLogs);
                context.Database.ExecuteSqlRaw(""); // force sans intercepteur
                base.SavedChanges(eventData, result);
            }

            return result;
        }
    }
}