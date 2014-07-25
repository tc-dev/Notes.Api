using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Infrastructure.EntityFramework;

namespace Notes.Api.Infrastructure.EntityFramework.Repositories
{
    public class DomainUserRepository
        : PersistableRepository<DomainUser>, IDomainUserRepository
    {
        public DomainUserRepository(IDbContext context) : base(context) {
        }

        public IDomainUserQueryBuilder Query() {
            return new DomainUserQueryBuilder(DbSet);
        }
    }
}
