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
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class NoteController : Controller
    {
        private readonly INotesManager manager;
        private readonly UserContext _dbContext;
        public NoteController(INotesManager notesManager, UserContext dbContext)
        {
            this.manager = notesManager;
            _dbContext = dbContext;
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
        public IActionResult UpdateNotes([FromBody] NoteModel noteData)
        {
            try
            {
                string result = this.manager.UpdateNotes(noteData);
                if (result.Equals("Successfully updated note"))
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
    }
}
