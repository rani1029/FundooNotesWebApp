using FundooModels;
using FundooRepository.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly UserContext userContext;
        public CollaboratorRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }

        public async Task<string> AddCollaborator(CollaboratorModel collaboratorUser)
        {
            try
            {
                var result = this.userContext.Notes.Where(x => x.NoteId == collaboratorUser.NoteId);
                if (result != null)
                {
                    this.userContext.Collaborators.Add(collaboratorUser);
                    await this.userContext.SaveChangesAsync();
                    return "new Collaborator Added";
                }
                else
                    return "No such Note found!";

            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
