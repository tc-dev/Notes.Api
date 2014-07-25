using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Data;

namespace Notes.Api.Core.Data.Repositories
{
    public interface INoteBookQueryBuilder
        : IQueryBuilder<NoteBook, INoteBookQueryBuilder>
    {
        INoteBookQueryBuilder ByOwnerId(int ownerId);

        INoteBookQueryBuilder BySharedWithDomainUserId(int domainUserId);

        INoteBookQueryBuilder ByTitle(string title);

        INoteBookQueryBuilder WhereTitleContains(string title);

        INoteBookQueryBuilder ByPageId(int pageId);
    }
}