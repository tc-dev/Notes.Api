using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Infrastructure.EntityFramework.Configurations;

namespace Notes.Api.Infrastructure.EntityFramework.Configurations
{
    public class NoteBookConfiguration : BaseEntityConfiguration<NoteBook>
    {
        public NoteBookConfiguration() {

            Property(m => m.OwnerId)
                .IsRequired();

            HasMany(m => m.SharedWith);

            Property(m => m.Title)
                .IsRequired();

            HasMany(m => m.Pages);

        }
    }
}