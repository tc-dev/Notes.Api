using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Data;

namespace Notes.Api.Core.Data.Repositories
{
    public interface IDomainUserQueryBuilder
        : IQueryBuilder<DomainUser, IDomainUserQueryBuilder>
    {
        IDomainUserQueryBuilder ByIdentityUserId(int identityUserId);

        IDomainUserQueryBuilder ByOwnedNoteBookId(int noteBookId);


        IDomainUserQueryBuilder WhereOwned();

        IDomainUserQueryBuilder WhereShared();
        IDomainUserQueryBuilder BySharedNoteBooksId(int noteBookId);
    }
}