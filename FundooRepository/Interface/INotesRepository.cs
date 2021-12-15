using FundooModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public interface INotesRepository
    {
        string CreateNote(NoteModel noteData);
        Task<string> UpdateNote(NoteModel model);
        bool DeleteNote(string email, int NoteId);
        bool ArchiveUnArchive(string email, int NoteId);
        bool PinUnpin(string email, int NoteId);
        bool TrashUntrash(string email, int noteid);
        public IEnumerable<NoteModel> GetArchivedNoteList(string email);
        public Task<string> AddImage(int noteId, IFormFile form);
        IEnumerable<NoteModel> GetNote(int noteid);
        public Task<string> AddReminder(int notesId, string remind);
        Task<string> EditColor(int noteId, string color);
        IEnumerable<NoteModel> GetAllNotes(int userId);
    }
}