using tc_dev.Core.Domain;

namespace Notes.Api.Core.Domain.Models
{
    public class Note : BaseEntity
    {
        public int PageId { get; set; }
        public Page Page { get; set; }

        public string Text { get; set; }
    }
}