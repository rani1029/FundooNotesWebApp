using FundooModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class collaboratorModel
    {
        [Key]
        public int CollaboratorID { get; set; }

        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }

        public NoteModel noteModel { get; set; }

        public string CollaboratorEmail { get; set; }
    }
}
