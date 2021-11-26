using FundooModel;
using FundooRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepository repository;

        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }
        public string CreateNote(NoteModel noteData)
        {
            return repository.CreateNote(noteData);
        }

        public string UpdateNotes(NoteModel model)
        {
            return repository.UpdateNotes(model);
        }
    }
}
