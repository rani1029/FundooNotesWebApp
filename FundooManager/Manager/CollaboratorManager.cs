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
        public async Task<string> AddCollaborator(CollaboratorModel collaboratorUser)
        {
            return await repository.AddCollaborator(collaboratorUser);
        }



    }
}
