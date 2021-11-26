using FundooModel;
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
        IQueryable<NoteModel> GetAllNotes(string Email);
    }
}