using System.Linq;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Infrastructure.EntityFramework;

namespace Notes.Api.Infrastructure.EntityFramework.Repositories
{
    public class DomainUserQueryBuilder
        : QueryBuilder<DomainUser, IDomainUserQueryBuilder>, IDomainUserQueryBuilder
    {
        public DomainUserQueryBuilder(IQueryable<DomainUser> query) : base(query) {
        }

        public IDomainUserQueryBuilder ByIdentityUserId(int identityUserId) {
            Query = Query.Where(m => m.IdentityUserId == identityUserId);

            return this;
        }

        public IDomainUserQueryBuilder ByOwnedNoteBookId(int noteBookId) {
            Query = Query.Where(m => m.OwnedNoteBooks.Any(o => o.Id == noteBookId));

            return this;
        }

        public IDomainUserQueryBuilder BySharedNoteBooksId(int noteBookId) {
            Query = Query.Where(m => m.SharedNoteBooks.Any(s => s.Id == noteBookId));

            return this;
        }
    }
}