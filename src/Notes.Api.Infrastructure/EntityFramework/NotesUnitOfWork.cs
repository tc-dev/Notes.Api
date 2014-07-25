using System;
using System.Threading;
using System.Threading.Tasks;
using Notes.Api.Core.Data;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Infrastructure.EntityFramework.Repositories;
using tc_dev.Core.Common.Utilities;
using tc_dev.Core.Infrastructure.EntityFramework;

namespace Notes.Api.Infrastructure.EntityFramework
{
    public class NotesUnitOfWork : INotesUnitOfWork
    {
        private readonly IDbContext _context;
        private bool _disposed;

        public NotesUnitOfWork(IDbContext context) {
            context.ThrowIfNull("context");

            _context = context;
        }

        private IDomainUserRepository _domainUserRepository;
        public IDomainUserRepository DomainUserRepository {
            get {
                return _domainUserRepository ??
                        (_domainUserRepository = new DomainUserRepository(_context));
            }
        }

        private INoteBookRepository _noteBookRepository;
        public INoteBookRepository NoteBookRepository {
            get {
                return _noteBookRepository ??
                       (_noteBookRepository = new NoteBookRepository(_context));
            }
        }

        private IPageRepository _pageRepository;
        public IPageRepository PageRepository {
            get {
                return _pageRepository ??
                        (_pageRepository = new PageRepository(_context));
            }
        }

        private INoteRepository _noteRepository;
        public INoteRepository NoteRepository {
            get {
                return _noteRepository ??
                        (_noteRepository = new NoteRepository(_context));
            }
        }

        public int SaveChanges() {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync() {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void BeginTransaction() {
            _context.BeginTransaction();
        }

        public int Commit() {
            return _context.Commit();
        }

        public void Rollback() {
            _context.Rollback();
        }

        public Task<int> CommitAsync() {
            return _context.CommitAsync();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing) {
            if (!_disposed && disposing) {
                _context.Dispose();

                _domainUserRepository.Dispose();
                _noteRepository.Dispose();
                _pageRepository.Dispose();
                _noteRepository.Dispose();
            }
            _disposed = true;
        }
    }
}
