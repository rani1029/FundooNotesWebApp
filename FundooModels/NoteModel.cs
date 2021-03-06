using FundooModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModel
{
    public class NoteModel
    {
        [Key]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
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
        [ForeignKey("UserId")] ////As it creates relationship with Users table that this note should belongs to perticular userid
        public RegisterModel registerModel { get; set; }
    }
}
