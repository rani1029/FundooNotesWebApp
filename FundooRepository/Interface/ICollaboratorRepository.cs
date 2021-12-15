using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public interface ICollaboratorRepository
    {
        public bool AddCollaborator(CollaboratorModel collaboratorUser);
        string DeleteCollaborator(int collabId);
        IEnumerable<string> GetCollaborators(int noteId);
    }
}