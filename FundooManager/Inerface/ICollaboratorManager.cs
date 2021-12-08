using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public interface ICollaboratorManager
    {
        public Task<string> AddCollaborator(CollaboratorModel collaboratorUser);
        Task<string> DeleteCollaborator(int noteId, string collabMail);
    }
}