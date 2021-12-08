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
        string UpdateNote(NoteModel model);
        // NoteModel GetNote(string email, int id);
        // IQueryable<NoteModel> GetAllNotes(string Email);
        bool DeleteNote(string email, int NoteId);
        bool ArchiveUnArchive(string email, int NoteId);
        bool PinUnpin(string email, int NoteId);
        bool TrashUntrash(string email, int noteid);
        public IEnumerable<NoteModel> GetArchivedNoteList(string email);
        public Task<string> AddImage(int noteId, IFormFile form);


        Task<string> EditColor(NoteModel noteData);
        //string AddImage(int noteId, IFormFile form);
        //IEnumerable<NoteModel> GetArchiveNotes(string email);
    }
}