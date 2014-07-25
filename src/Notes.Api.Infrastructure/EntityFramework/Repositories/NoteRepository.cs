using System;
using System.Data.Entity;
using System.Linq;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Common.Utilities;
using tc_dev.Core.Infrastructure.EntityFramework;

namespace Notes.Api.Infrastructure.EntityFramework.Repositories
{
    public class NoteRepository
        : PersistableRepository<Note>, INoteRepository
    {
        public NoteRepository(IDbContext context) : base(context) {
        }

        public INoteQueryBuilder Query() {
            return new NoteQueryBuilder(DbSet);
        }
    }

    public class NoteQueryBuilder 
        : QueryBuilder<Note, INoteQueryBuilder>, INoteQueryBuilder
    {
        public NoteQueryBuilder(IQueryable<Note> query) : base(query) {
            
        }

        public INoteQueryBuilder ByPageId(int pageId) {
            Query = Query.Where(m => m.PageId == pageId);

            return this;
        }

        public INoteQueryBuilder ByText(string text) {
            text.ThrowIfNull("text");

            Query = Query.Where(m =>
                string.Equals(m.Text, text, StringComparison.CurrentCultureIgnoreCase));

            return this;
        }

        public INoteQueryBuilder WhereTextContains(string val) {
            val.ThrowIfNull("val");

            Query = Query.Where(m => m.Text.ToLower().Contains(val.ToLower()));

            return this;
        }
    }
}
