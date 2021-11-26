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
        public string UserEmail { get; set; }
        public string Title { get; set; }
        public string MyNote { get; set; }
        public bool DeleteNote { get; set; }
        public string AddLabel { get; set; }
        public bool Pin { get; set; }
        public string Colors { get; set; }
        public string RemindMe { get; set; }
        public bool Archive { get; set; }
        public string Trash { get; set; }
        public DateTime Date { get; set; }
    }
}
