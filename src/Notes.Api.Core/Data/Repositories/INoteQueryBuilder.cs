using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Data;

namespace Notes.Api.Core.Data.Repositories
{
    public interface INoteQueryBuilder
        : IQueryBuilder<Note, INoteQueryBuilder>
    {
        INoteQueryBuilder ByPageId(int pageId);

        INoteQueryBuilder ByText(string text);

        INoteQueryBuilder WhereTextContains(string val);
    }
}