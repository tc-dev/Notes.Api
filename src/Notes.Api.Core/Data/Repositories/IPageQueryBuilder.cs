using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Data;

namespace Notes.Api.Core.Data.Repositories
{
    public interface IPageQueryBuilder
        : IQueryBuilder<Page, IPageQueryBuilder>
    {
        IPageQueryBuilder ByNoteBookId(int noteBookId);

        IPageQueryBuilder ByColorHex(string hex);

        IPageQueryBuilder ByTitle(string title);

        IPageQueryBuilder WhereTitleContains(string val);

        IPageQueryBuilder ByNoteId(int noteId);
    }
}