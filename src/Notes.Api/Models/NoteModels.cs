using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Api.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        public int PageId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }

        public string Text { get; set; }
    }

    public class NoteBindingModel
    {
        [Required]
        public int PageId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}