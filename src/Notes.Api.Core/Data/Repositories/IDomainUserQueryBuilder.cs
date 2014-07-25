using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Data;

namespace Notes.Api.Core.Data.Repositories
{
    public interface IDomainUserQueryBuilder
        : IQueryBuilder<DomainUser, IDomainUserQueryBuilder>
    {
        IDomainUserQueryBuilder ByIdentityUserId(int identityUserId);

        IDomainUserQueryBuilder ByOwnedNoteBookId(int noteBookId);

        IDomainUserQueryBuilder BySharedNoteBooksId(int noteBookId);
    }
}