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

    //[Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INotesManager manager;

        public NoteController(INotesManager notesManager)
        {
            this.manager = notesManager;

        }
        /// <summary>
        /// api to create new note
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns>response </returns>
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
        /// <summary>
        /// api to update note
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns>reponse</returns>
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

        [HttpGet]
        [Route("api/GetNote")]
        public IActionResult GetNote(int note)
        {
            try
            {
                IEnumerable<NoteModel> result = this.manager.GetNote(note);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NoteModel>>() { Status = true, Message = "Successfully Retrieved", Data = result });
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

        /// <summary>
        /// api to archive and unarhive
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns>response</returns>
        [HttpPut]
        [Route("api/ArchiveNote")]
        public IActionResult ArchiveUnArchiveNote(string email, int NoteId)
        {
            try
            {
                bool result = this.manager.ArchiveUnArchive(email, NoteId);
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
        /// <summary>
        /// api to pin and unpin note
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns>response</returns>
        [HttpPut]
        [Route("api/PinUnpinNote")]
        public IActionResult PinUnpinNote(string email, int NoteId)
        {
            try
            {
                bool result = this.manager.PinUnpin(email, NoteId);
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
        /// <summary>
        /// api to trash and untrash note
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/TrashUntrashNote")]
        public IActionResult TrashUntrash(string email, int NoteId)
        {
            try
            {
                bool result = this.manager.TrashUntrash(email, NoteId);
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
        /// <summary>
        /// api to delete note 
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns>reponse</returns>
        [HttpDelete]
        [Route("api/DeleteNote")]
        public IActionResult DeleteNote(string email, int id)
        {
            try
            {
                bool result = this.manager.DeleteNote(email, id);
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
        /// <summary>
        /// api to get all archived note 
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns>response</returns>
        [HttpGet]
        [Route("api/GetArchivedNoteList")]
        public IActionResult GetArchivedNoteList(string email)
        {
            try
            {
                IEnumerable<NoteModel> result = this.manager.GetArchivedNoteList(email);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NoteModel>>() { Status = true, Message = "Successfully Retrieved", Data = result });
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

        /// <summary>
        /// api to change note color
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns>response</returns>
        [HttpPut]
        [Route("api/editColor")]
        public async Task<IActionResult> EditColor(int noteId, string color)
        {
            try
            {
                string result = await this.manager.EditColor(noteId, color);
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
        /// <summary>
        /// api to add remind me 
        /// </summary>
        /// <param name="notesId"></param>
        /// <param name="remind"></param>
        /// <returns>response</returns>
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
        /// <summary>
        /// api to add image
        /// </summary>
        /// <param name="notesId"></param>
        /// <param name="image"></param>
        /// <returns>response</returns>
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

        [HttpGet]
        [Route("api/getallnotes")]
        public IActionResult GetAllNotes(int userId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.manager.GetAllNotes(userId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No note exists whose data is data is to be retrieved" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<NoteModel>>() { Status = true, Message = "Data for all notes is retrived successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
