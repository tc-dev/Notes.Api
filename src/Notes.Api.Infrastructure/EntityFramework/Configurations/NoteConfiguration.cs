using Notes.Api.Core.Domain.Models;
using tc_dev.Core.Infrastructure.EntityFramework.Configurations;

namespace Notes.Api.Infrastructure.EntityFramework.Configurations
{
    public class NoteConfiguration : BaseEntityConfiguration<Note>
    {
        public NoteConfiguration() {

            Property(m => m.PageId)
                .IsRequired();

            Property(m => m.Text)
                .IsRequired();

        }
    }
}