using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue(false)]
        public bool IsArchive { get; set; }
        [DefaultValue(false)]
        public bool IsTrash { get; set; }
        [DefaultValue(false)]
        public bool IsPin { get; set; }
    }
}
