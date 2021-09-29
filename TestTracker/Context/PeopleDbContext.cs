using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TestTracker.Models;

namespace TestTracker.Context
{
    public class PeopleDbContext : DbContext
    {

        #region Normal DbSet
        public DbSet<Person> People { get; set; }
        #endregion

        #region Required Variables To Store Audits
        private ICollection<AuditEntry> TemporaryAudits = new List<AuditEntry>(); // Store temporary entities for later saving
        public DbSet<AuditLog> Audits { get; set; } // The DbSet for storing audits (change logs)
        #endregion

        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        {
            #region Event trigger to catch changes
            ChangeTracker.Tracked += StoreTrack;
            ChangeTracker.StateChanged += StoreTrack;
            SavedChanges += SaveTemporaryAudits;
            #endregion
        }

        #region Destructor to clear the temporary audits (Optional)
        ~PeopleDbContext()
        {
            TemporaryAudits.Clear();
        }
        #endregion

        /// <summary>
        /// Load temporary properties and save temporary audits to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SaveTemporaryAudits(object sender, SavedChangesEventArgs args)
        {
            if (!TemporaryAudits.Any())
            {
                return;
            }
            foreach (var auditEntry in TemporaryAudits)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                Audits.Add(auditEntry.ToAudit());
            }
            TemporaryAudits.Clear();
            SaveChanges();
        }

        /// <summary>
        /// Catch entity changes and create audit logs for each entities.
        /// Some entities may have temporary values (values generated in database).
        /// Those temporary entities will be stored in a collection of temporary audits for later saving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void StoreTrack(object sender, EntityEntryEventArgs args)
        {
            var entry = args.Entry;
            // Ignore unwanted states
            if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged) return;

            var auditEntry = new AuditEntry() {Entry = entry};
            auditEntry.TableName = entry.Metadata.GetTableName();

            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    auditEntry.TemporaryProperties.Add(property);
                    continue;
                }

                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;

                    case EntityState.Deleted:
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }
            // Only store completed audits
            if (!auditEntry.HasTemporaryProperties)
                Audits.Add(auditEntry.ToAudit());
            else
                TemporaryAudits.Add(auditEntry);
        }
    }
}