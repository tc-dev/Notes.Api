using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Infrastructure.EntityFramework.Configurations;

namespace Notes.Api.Infrastructure.EntityFramework.Configurations
{
    public class PageConfiguration : BaseEntityConfiguration<Page>
    {
        public PageConfiguration() {

            Property(m => m.NoteBookId)
                .IsRequired();

            Property(m => m.ColorHex)
                .IsRequired();

            Property(m => m.Title)
                .IsRequired();

            HasMany(m => m.Notes);

        }
    }
}