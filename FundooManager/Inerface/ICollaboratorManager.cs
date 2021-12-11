using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public interface ICollaboratorManager
    {
        public bool AddCollaborator(CollaboratorModel collaboratorUser);
        public string DeleteCollaborator(int CollabId);
        public IEnumerable<string> GetCollaborators(int noteId);
    }
}