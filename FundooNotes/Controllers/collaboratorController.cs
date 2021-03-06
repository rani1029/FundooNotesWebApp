using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooManager.Manager;
using FundooModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{

    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager manager;

        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.manager = collaboratorManager;

        }

        /// <summary>
        /// api to add new collaborator
        /// </summary>
        /// <param name="collaboratorUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/addcollaborator")]
        public IActionResult AddCollaborator([FromBody] CollaboratorModel collaboratorUser)
        {
            try
            {
                bool result = this.manager.AddCollaborator(collaboratorUser);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "collaborator added." });
                }
                else
                    return this.BadRequest(new ResponseModel<string>()
                    {
                        Status = false,
                        Message = "Collaborator cannot be added"
                    });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// api to deletes label
        /// </summary>
        /// <param name="CollabId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/deletecollaborator")]
        public IActionResult DeleteCollaborator(int CollabId)
        {
            try
            {
                string result = this.manager.DeleteCollaborator(CollabId);
                if (result.Equals("Collaborator Removed"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// api to get all collaborator of note
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getCollaborator")]
        public IActionResult GetCollaborator(int noteid)
        {
            try
            {
                IEnumerable<string> result = this.manager.GetCollaborators(noteid);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No such collaborator found" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<string>>() { Status = true, Message = "Successfully Retrieved", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }

        }
    }
}
