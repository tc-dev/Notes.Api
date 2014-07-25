using System.Collections.Generic;
using tc_dev.Core.Domain;

namespace Notes.Api.Core.Domain.Models
{
    public class Page : BaseEntity
    {
        public int NoteBookId { get; set; }
        public NoteBook NoteBook { get; set; }

        public string ColorHex { get; set; }

        public string Title { get; set; }

        public ICollection<Note> Notes { get; set; }

        public Page() {
            Notes = new List<Note>();
        }
    }
}