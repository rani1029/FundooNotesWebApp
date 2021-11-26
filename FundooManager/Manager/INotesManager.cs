using FundooModel;

namespace FundooManager.Manager
{
    public interface INotesManager
    {
        string CreateNote(NoteModel noteData);
        string UpdateNotes(NoteModel noteData);
    }
}