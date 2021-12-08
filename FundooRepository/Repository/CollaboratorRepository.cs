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
        public async Task<string> DeleteCollaborator(int noteId, string collabMail)
        {
            try
            {
                ////It delete perticular collab mail wrt note id
                var removeCollab = this.userContext.Collaborators.Where(x => x.NoteId == noteId && x.CollaboratorEmail == collabMail).SingleOrDefault();
                if (removeCollab != null)
                {
                    this.userContext.Collaborators.Remove(removeCollab);
                    await this.userContext.SaveChangesAsync();
                    return " Collaborator Removed";
                }
                else
                    return "No such Collaborator found!";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
