using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ChangeLogInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

            var logs = new List<History>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    var tableName = entry.Entity.GetType().Name;

                    var primaryKeys = entry.Metadata.FindPrimaryKey();
                    var keyValues = primaryKeys?.Properties.Select(p => entry.CurrentValues[p]).ToArray();
                    var recordId = keyValues != null ? string.Join(",", keyValues) : "Unknown";


                    var changeType = entry.State.ToString();

                    var oldData = entry.State == EntityState.Modified || entry.State == EntityState.Deleted
                        ? JsonSerializer.Serialize(entry.OriginalValues.Properties.ToDictionary(p => p.Name, p => entry.OriginalValues[p]))
                        : null;

                    var newData = entry.State == EntityState.Added || entry.State == EntityState.Modified
                        ? JsonSerializer.Serialize(entry.CurrentValues.Properties.ToDictionary(p => p.Name, p => entry.CurrentValues[p]))
                        : null;

                    logs.Add(new History
                    {
                        TableName = tableName,
                        RecordId = recordId,
                        ChangeType = changeType,
                        OldData = oldData,
                        NewData = newData,
                        ChangeDate = DateTime.UtcNow
                    });
                }
            }

            if (logs.Any())
            {
                context.Set<History>().AddRange(logs);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
