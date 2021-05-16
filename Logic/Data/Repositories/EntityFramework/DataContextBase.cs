using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Logic.Infrastructures.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logic.Data.Repositories.EntityFramework
{
    public class DataContextBase: DbContext
    {
        protected bool SetUpdatedOnSameAsCreatedOnForNewObjects { get; set; }

        protected DataContextBase(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
        }

        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            HandleSoftDeletableEntities();
            HandleAuditableEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await SaveChangesAsync(true, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            HandleSoftDeletableEntities();
            HandleAuditableEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void HandleSoftDeletableEntities()
        {
            IEnumerable<EntityEntry> entries = ChangeTracker.Entries().Where(x => x.Entity is IDeletable && x.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                IDeletable entity = (IDeletable)entry.Entity;
                entity.IsDeleted = true;
            }
        }

        private void HandleAuditableEntities()
        {
            string currentUser = Thread.CurrentPrincipal?.Identity?.Name;

            IEnumerable<EntityEntry> entities = ChangeTracker.Entries().Where(x => x.Entity is IAuditable && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (EntityEntry entry in entities)
            {
                if (entry.Entity is IAuditable)
                {
                    IAuditable auditable = ((IAuditable)entry.Entity);

                    if (entry.State == EntityState.Added)
                    {
                        if (auditable.CreatedDate == DateTime.MinValue)
                        {
                            auditable.CreatedDate = DateTime.Now;

                            if (SetUpdatedOnSameAsCreatedOnForNewObjects)
                            {
                                auditable.UpdatedDate = auditable.CreatedDate;
                            }
                        }

                        if (String.IsNullOrEmpty(auditable.CreatedBy))
                        {
                            auditable.CreatedBy = currentUser;

                            if (SetUpdatedOnSameAsCreatedOnForNewObjects)
                            {
                                auditable.UpdatedBy = auditable.CreatedBy;
                            }
                        }
                    }
                    else
                    {
                        auditable.UpdatedDate = DateTime.Now;
                        auditable.UpdatedBy = currentUser;
                    }
                }
            }
        }
    }
}