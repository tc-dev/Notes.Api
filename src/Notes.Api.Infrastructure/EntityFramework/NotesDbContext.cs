using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using tc_dev.Core.Domain;
using tc_dev.Core.Infrastructure.EntityFramework;
using tc_dev.Core.Infrastructure.EntityFramework.Conventions;
using tc_dev.Core.Infrastructure.Identity.Models;
using tc_dev.Core.Service;

namespace Notes.Api.Infrastructure.EntityFramework
{
    public class NotesDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, int, AppIdentityUserLogin, AppIdentityUserRole, AppIdentityUserClaim>, IDbContext
    {
        private ObjectContext _objectContext;
        private DbTransaction _transaction;

        public NotesDbContext()
            : base("AppContext") {
        }

        public NotesDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) 
        {
        }

        public NotesDbContext(string nameOrConnectionString, ILogger logger)
            : this(nameOrConnectionString) 
        {
            Database.Log = logger.Log;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add<ForeignKeyNamingConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        public void BeginTransaction() {
            _objectContext = ((IObjectContextAdapter)this).ObjectContext;

            if (_objectContext.Connection.State == ConnectionState.Open)
                return;

            _objectContext.Connection.Open();
            _transaction = _objectContext.Connection.BeginTransaction();
        }

        public int Commit() {
            var saveChanges = SaveChanges();
            _transaction.Commit();

            return saveChanges;
        }

        public Task<int> CommitAsync() {
            var saveChangesAsync = SaveChangesAsync();
            _transaction.Commit();

            return saveChangesAsync;
        }

        public void Rollback() {
            _transaction.Rollback();
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : BaseEntity {
            var dbEntityEntry = GetDbEntry(entity);
            dbEntityEntry.State = state;
        }

        private DbEntityEntry GetDbEntry<TEntity>(TEntity entity) where TEntity : BaseEntity {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
                Set<TEntity>().Attach(entity);

            return dbEntityEntry;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                    _objectContext.Connection.Close();
                if (_objectContext != null)
                    _objectContext.Dispose();
                if (_transaction != null)
                    _transaction.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
