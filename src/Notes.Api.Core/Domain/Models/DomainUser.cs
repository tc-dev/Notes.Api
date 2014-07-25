using System.Collections.Generic;
using tc_dev.Core.Domain;

namespace Notes.Api.Core.Domain.Models
{
    public class DomainUser : BaseEntity
    {
        public int IdentityUserId { get; set; }

        public ICollection<NoteBook> OwnedNoteBooks { get; set; }

        // the notebooks shared with this user
        public ICollection<NoteBook> SharedNoteBooks { get; set; }

        public DomainUser() {
            OwnedNoteBooks = new List<NoteBook>();
            SharedNoteBooks = new List<NoteBook>();
        }
    }
}
