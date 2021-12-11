using FundooModel;
using FundooModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public interface INotesManager
    {
        string CreateNote(NoteModel noteData);
        string UpdateNote(NoteModel model);
        bool ArchiveUnArchive(string email, int NoteId);
        bool PinUnpin(string email, int NoteId);
        bool TrashUntrash(string email, int NoteId);
        public IEnumerable<NoteModel> GetArchivedNoteList(string email);
        public Task<string> AddReminder(int notesId, string remind);
        Task<string> EditColor(int noteId, string color);
        bool DeleteNote(string email, int NoteId);
        public Task<string> AddImage(int noteId, IFormFile form);
        IEnumerable<NoteModel> GetNote(int note);
        IEnumerable<NoteModel> GetAllNotes(int userId);
        //Task<string> AddCollaborator(CollaboratorModel collaboratorUser);
    }
}