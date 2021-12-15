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

    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelmanager;

        public LabelController(ILabelManager labelmanager)
        {
            this.labelmanager = labelmanager;

        }

        /// <summary>
        /// api to add new label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/notelabel")]
        public async Task<IActionResult> AddLabel([FromBody] LabelModel label
            )
        {
            try
            {
                string result = await this.labelmanager.AddLabel(label);

                if (result.Equals("Label is added Successfully"))
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
        /// api to add label 
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddUserslabel")]
        public async Task<IActionResult> AddLabelbyUserId([FromBody] LabelModel label
            )
        {
            try
            {
                string result = await this.labelmanager.AddLabelsByUserId(label);

                if (result.Equals("Label is added Successfully"))
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
        /// api to remove label from note
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("removelabel")]
        public async Task<IActionResult> RemoveLabel(int labelId)
        {
            try
            {
                string result = await this.labelmanager.RemoveLabel(labelId);

                if (result.Equals("Deleted Label From Note"))
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
        /// api to delete label
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/deleteLabel")]
        public async Task<IActionResult> DeleteLabel(int userId, string labelName)
        {
            try
            {
                string result = await this.labelmanager.DeleteLabel(userId, labelName);
                if (result == "Deleted Label")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// api to get all label by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlabelbyUserid")]
        public IActionResult GetLabelByUserid(int userId)
        {
            try
            {
                IEnumerable<string> result = this.labelmanager.GetLabelByUserid(userId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No label found" });
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

        /// <summary>
        /// pi to get all label by note id
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlabelbynotes")]
        public IActionResult GetLabelByNote(int notesId)
        {
            try
            {
                IEnumerable<LabelModel> result = this.labelmanager.GetLabelByNote(notesId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No label found" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<LabelModel>>() { Status = true, Message = "Successfully Retrieved", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// edits existing label
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="labelName"></param>
        /// <param name="newLabelName"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/editLabel")]
        public async Task<IActionResult> EditLabel(int userId, string labelName, string newLabelName)
        {
            try
            {
                string result = await this.labelmanager.EditLabel(userId, labelName, newLabelName);
                if (result != "Label not present")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// api to edit label
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="labelName"></param>
        /// <param name="newLabelName"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/editLabelwithNoteId")]
        public async Task<IActionResult> EditLabelWithNoteId(int noteId, string labelName, string newLabelName)
        {
            try
            {
                string result = await this.labelmanager.EditLabel(noteId, labelName, newLabelName);
                if (result != "Label not present")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
