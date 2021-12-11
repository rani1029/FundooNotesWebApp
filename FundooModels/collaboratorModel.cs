using FundooModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    /// <summary>
    /// model class of collaborator
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int CollaboratorID { get; set; }
        public string UserEmail { get; set; }
        public int NoteId { get; set; }
        public string CollaboratorEmail { get; set; }
    }
}
