using System;
using System.Linq;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Common.Utilities;
using tc_dev.Core.Infrastructure.EntityFramework;

namespace Notes.Api.Infrastructure.EntityFramework.Repositories
{
    public class PageQueryBuilder 
        : QueryBuilder<Page, IPageQueryBuilder>, IPageQueryBuilder
    {
        public PageQueryBuilder(IQueryable<Page> query) : base(query) {
            
        }

        public IPageQueryBuilder ByNoteBookId(int noteBookId) {
            Query = Query.Where(m => m.NoteBookId == noteBookId);

            return this;
        }

        public IPageQueryBuilder ByColorHex(string hex) {
            hex.ThrowIfNull("hex");

            Query = Query.Where(m =>
                string.Equals(m.ColorHex, hex, StringComparison.CurrentCultureIgnoreCase));

            return this;
        }

        public IPageQueryBuilder ByTitle(string title) {
            title.ThrowIfNull("title");

            Query = Query.Where(m =>
                string.Equals(m.Title, title, StringComparison.CurrentCultureIgnoreCase));

            return this;
        }

        public IPageQueryBuilder WhereTitleContains(string val) {
            val.ThrowIfNull("val");

            Query = Query.Where(m => m.Title.ToLower().Contains(val.ToLower()));

            return this;
        }

        public IPageQueryBuilder ByNoteId(int noteId) {
            Query = Query.Where(m => m.Notes.Any(n => n.Id == noteId));

            return this;
        }
    }
}