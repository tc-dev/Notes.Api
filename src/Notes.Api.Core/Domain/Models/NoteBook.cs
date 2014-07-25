using System.Collections.Generic;
using tc_dev.Core.Domain;

namespace Notes.Api.Core.Domain.Models
{
    public class NoteBook : BaseEntity
    {
        public int OwnerId { get; set; }
        public DomainUser Owner { get; set; }

        // the users this notebook is shared with
        public ICollection<DomainUser> SharedWith { get; set; }

        public string Title { get; set; }

        public ICollection<Page> Pages { get; set; }

        public NoteBook() {
            SharedWith = new List<DomainUser>();
            Pages = new List<Page>();
        }
    }
}