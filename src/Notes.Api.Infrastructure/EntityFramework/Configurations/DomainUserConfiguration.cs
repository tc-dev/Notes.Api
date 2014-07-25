using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Infrastructure.EntityFramework.Configurations;

namespace Notes.Api.Infrastructure.EntityFramework.Configurations
{
    public class DomainUserConfiguration : BaseEntityConfiguration<DomainUser>
    {
        public DomainUserConfiguration() {
            
            Property(m => m.IdentityUserId)
                .IsRequired();

            HasMany(m => m.OwnedNoteBooks);

            HasMany(m => m.SharedNoteBooks);

        }
    }
}
