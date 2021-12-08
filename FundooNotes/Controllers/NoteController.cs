using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooManager.Manager;
using FundooModel;
using FundooModels;
using FundooRepository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
    //[Produces("application/json")]
    //[Route("[controller]")]
    //[ApiController]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class NoteController : ControllerBase
    {
        private readonly INotesManager manager;

        public NoteController(INotesManager notesManager)
        {
            this.manager = notesManager;

        }

        [HttpPost]
        [Route("api/Createnote")]
        public IActionResult CreateNote([FromBody] NoteModel noteData)
        {
            try
            {
                string result = this.manager.CreateNote(noteData);
                if (result.Equals("Successfully created note"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = " Session data" });
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
        [HttpPut]
        [Route("api/UpdateNotes")]
        public IActionResult UpdateNote([FromBody] NoteModel noteData)
        {
            try
            {
                string result = this.manager.UpdateNote(noteData);
                if (result.Equals("Note Updated Successfully!"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = " Session data" });
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
        [HttpPut]
        [Route("api/ArchiveNote")]
        public IActionResult ArchiveUnArchiveNote([FromBody] NoteModel noteData)
        {
            try
            {
                bool result = this.manager.ArchiveUnArchive(noteData.Email, noteData.NoteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "successful", Data = " Session data" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/PinUnpinNote")]
        public IActionResult PinUnpinNote([FromBody] NoteModel noteData)
        {
            try
            {
                bool result = this.manager.PinUnpin(noteData.Email, noteData.NoteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "successful", Data = " Session data" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/TrashUntrashNote")]
        public IActionResult TrashUntrash([FromBody] NoteModel noteData)
        {
            try
            {
                bool result = this.manager.TrashUntrash(noteData.Email, noteData.NoteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "successful", Data = " Session data" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("api/DeleteNote")]
        public IActionResult DeleteNote([FromBody] NoteModel noteData)
        {
            try
            {
                bool result = this.manager.DeleteNote(noteData.Email, noteData.NoteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "successful", Data = " Session data" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "failed" });
                }
            }


            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("api/GetArchivedNoteList")]
        public IActionResult GetArchivedNoteList(NoteModel noteData)
        {
            try
            {
                IEnumerable<NoteModel> result = this.manager.GetArchivedNoteList(noteData.Email);
                if (result != null)
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


        [HttpPut]
        [Route("api/editColor")]
        public async Task<IActionResult> EditColor([FromBody] NoteModel noteData)
        {
            try
            {
                string result = await this.manager.EditColor(noteData);
                if (result.Equals("Successfully change color"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = " Session data" });
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
        [HttpPut]
        [Route("api/addremindme")]
        public async Task<IActionResult> AddRemindMe(int notesId, string remind)
        {
            try
            {
                string result = await this.manager.AddReminder(notesId, remind);
                if (result.Equals("Reminder added successfully"))
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
        [HttpPut]
        [Route("api/addImage")]
        public async Task<IActionResult> AddImage(int notesId, IFormFile image)
        {
            try
            {
                string result = await this.manager.AddImage(notesId, image);
                if (result == "Image added Successfully")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = true, Message = ex.Message });
            }
        }
    }
}
