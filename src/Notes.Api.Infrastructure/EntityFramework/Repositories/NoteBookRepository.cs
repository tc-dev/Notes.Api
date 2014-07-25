using System.Data.Entity;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Infrastructure.EntityFramework;

namespace Notes.Api.Infrastructure.EntityFramework.Repositories
{
    public class NoteBookRepository
        : PersistableRepository<NoteBook>, INoteBookRepository
    {
        public NoteBookRepository(IDbContext context) : base(context) {
        }

        public INoteBookQueryBuilder Query() {
            return new NoteBookQueryBuilder(DbSet);
        }
    }
}
