using FundooModels;
using FundooRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository repository;
        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }
        public bool AddCollaborator(CollaboratorModel collaboratorUser)
        {
            return repository.AddCollaborator(collaboratorUser);
        }
        public string DeleteCollaborator(int CollabId)
        {
            return repository.DeleteCollaborator(CollabId);
        }
        public IEnumerable<string> GetCollaborators(int noteId)
        {
            return repository.GetCollaborators(noteId);
        }



    }
}
