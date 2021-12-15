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
    /// <summary>
    /// repository class of collaborator
    /// </summary>
    public class CollaboratorRepository : ICollaboratorRepository
    {
        /// <summary>
        /// user context object
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userContext"></param>
        public CollaboratorRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// method to add new collaborator
        /// </summary>
        /// <param name="collaboratorUser"></param>
        /// <returns></returns>
        public bool AddCollaborator(CollaboratorModel collaboratorUser)
        {
            try
            {
                var result = this.userContext.Notes.Where(x => x.NoteId == collaboratorUser.NoteId);
                if (result != null)
                {
                    this.userContext.Collaborators.Add(collaboratorUser);
                    this.userContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// deletes collaborator
        /// </summary>
        /// <param name="collabId"></param>
        /// <returns>delete successful or not in string</returns>
        public string DeleteCollaborator(int collabId)
        {
            try
            {
                var removeCollab = this.userContext.Collaborators.Where(x => x.CollaboratorID == collabId).SingleOrDefault();
                if (removeCollab != null)
                {
                    this.userContext.Collaborators.Remove(removeCollab);
                    this.userContext.SaveChanges();
                    return "Collaborator Removed";
                }
                else
                {
                    return "No such Collaborator found!";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// gets all the collaborator for one noteid
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns>collaborators email for one note</returns>
        public IEnumerable<string> GetCollaborators(int noteId)
        {
            try
            {
                IEnumerable<string> collaborators = from note in this.userContext.Collaborators where note.NoteId == noteId select note.CollaboratorEmail;
                if (collaborators != null)
                {
                    return collaborators;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
