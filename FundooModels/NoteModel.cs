using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModel
{
    public class NoteModel
    {
        [Key]
        public int NoteId { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Image { get; set; }
        public string Colour { get; set; }
        public string Reminder { get; set; }
        public bool IsArchive { get; set; }
        public bool IsTrash { get; set; }
        public bool IsPin { get; set; }
    }
}
