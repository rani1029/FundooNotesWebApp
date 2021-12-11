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
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelmanager;

        public LabelController(ILabelManager labelmanager)
        {
            this.labelmanager = labelmanager;

        }
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
