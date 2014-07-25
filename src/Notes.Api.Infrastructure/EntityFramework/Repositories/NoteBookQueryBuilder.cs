using System;
using System.Linq;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Common.Utilities;
using tc_dev.Core.Infrastructure.EntityFramework;

namespace Notes.Api.Infrastructure.EntityFramework.Repositories
{
    public class NoteBookQueryBuilder
        : QueryBuilder<NoteBook, INoteBookQueryBuilder>, INoteBookQueryBuilder
    {
        public NoteBookQueryBuilder(IQueryable<NoteBook> query) : base(query) {
            
        }

        public INoteBookQueryBuilder ByOwnerId(int ownerId) {
            Query = Query.Where(m => m.OwnerId == ownerId);

            return this;
        }

        public INoteBookQueryBuilder BySharedWithDomainUserId(int domainUserId) {
            Query = Query.Where(m => m.SharedWith.Any(u => u.Id == domainUserId));

            return this;
        }

        public INoteBookQueryBuilder ByTitle(string title) {
            title.ThrowIfNull("title");

            Query = Query.Where(m =>
                string.Equals(m.Title, title, StringComparison.CurrentCultureIgnoreCase));

            return this;
        }

        public INoteBookQueryBuilder WhereTitleContains(string title) {
            title.ThrowIfNull("title");

            Query = Query.Where(m => m.Title.ToLower().Contains(title.ToLower()));

            return this;
        }

        public INoteBookQueryBuilder ByPageId(int pageId) {
            Query = Query.Where(m => m.Pages.Any(p => p.Id == pageId));

            return this;
        }
    }
}