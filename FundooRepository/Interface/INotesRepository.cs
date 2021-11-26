using FundooModel;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public interface INotesRepository
    {
        // IConfiguration Configuration { get; }

        string CreateNote(NoteModel noteData);
        string UpdateNotes(NoteModel model);
    }
}