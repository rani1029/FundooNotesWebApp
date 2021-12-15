using FundooModel;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Http;
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

        public async Task<string> UpdateNote(NoteModel model)
        {
            return await repository.UpdateNote(model);
        }
        public IEnumerable<NoteModel> GetNote(int Noteid)
        {
            return repository.GetNote(Noteid);
        }
        public bool ArchiveUnArchive(string email, int NoteId)
        {
            return repository.ArchiveUnArchive(email, NoteId);
        }
        public IEnumerable<NoteModel> GetArchivedNoteList(string email)
        {
            return repository.GetArchivedNoteList(email);
        }
        public bool PinUnpin(string email, int NoteId)
        {
            return repository.PinUnpin(email, NoteId);
        }
        public bool TrashUntrash(string email, int NoteId)
        {
            return repository.TrashUntrash(email, NoteId);
        }

        public async Task<string> EditColor(int noteId, string color)
        {
            try { return await this.repository.EditColor(noteId, color); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> AddReminder(int notesId, string remind)
        {
            return await repository.AddReminder(notesId, remind);
        }
        public async Task<string> AddImage(int noteId, IFormFile form)
        {
            return await repository.AddImage(noteId, form);

        }

        public bool DeleteNote(string email, int NoteId)
        {
            try { return this.repository.DeleteNote(email, NoteId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteModel> GetAllNotes(int userId)
        {
            return repository.GetAllNotes(userId);
        }

    }
}
