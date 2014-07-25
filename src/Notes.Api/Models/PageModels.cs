using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notes.Api.Models
{
    public class PageModel
    {
        public int Id { get; set; }

        public int NoteBookId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }

        public string ColorHex { get; set; }

        public string Title { get; set; }

        public ICollection<NoteModel> Notes { get; set; }
    }

    public class PageBindingModel
    {
        [Required]
        public int NoteBookId { get; set; }

        [Required]
        public string ColorHex { get; set; }

        [Required]
        public string Title { get; set; }
    }
}