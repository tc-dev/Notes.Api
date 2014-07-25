using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Data;

namespace Notes.Api.Core.Data.Repositories
{
    public interface INoteBookRepository
        : IPersistableRepository<NoteBook>, IQueryableRepository<NoteBook, INoteBookQueryBuilder>
    {
    }
}
