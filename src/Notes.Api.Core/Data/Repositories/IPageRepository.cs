using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Data;

namespace Notes.Api.Core.Data.Repositories
{
    public interface IPageRepository
        : IPersistableRepository<Page>, IQueryableRepository<Page, IPageQueryBuilder>
    {
    }
}
