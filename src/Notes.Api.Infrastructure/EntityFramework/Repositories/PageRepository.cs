using System.Data.Entity;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Infrastructure.EntityFramework;

namespace Notes.Api.Infrastructure.EntityFramework.Repositories
{
    public class PageRepository
        : PersistableRepository<Page>, IPageRepository
    {
        public PageRepository(IDbContext context) : base(context) {
        }

        public IPageQueryBuilder Query() {
            return new PageQueryBuilder(DbSet);
        }
    }
}
