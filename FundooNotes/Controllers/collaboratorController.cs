﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooManager.Manager;
using FundooModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager manager;

        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.manager = collaboratorManager;

        }
        [HttpPost]
        [Route("api/addcollaborator")]
        public async Task<IActionResult> AddCollaborator([FromBody] CollaboratorModel collaboratorUser)
        {
            try
            {
                string result = await this.manager.AddCollaborator(collaboratorUser);
                if (result.Equals("new Collaborator Added"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("api/deletecollaborator")]
        public async Task<IActionResult> DeleteCollaborator(int noteId, string collabMail)
        {
            try
            {
                string result = await this.manager.DeleteCollaborator(noteId, collabMail);
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
        [HttpGet]
        [Route("api/getCollaborator")]
        public IActionResult GetCollaborator(int noteid)
        {
            try
            {
                IEnumerable<string> result = this.manager.GetCollaborator(noteid);

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
