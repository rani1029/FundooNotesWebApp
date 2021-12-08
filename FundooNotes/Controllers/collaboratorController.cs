using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooManager.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class collaboratorController : ControllerBase
    {
        private readonly INotesManager manager;

        public collaboratorController(INotesManager notesManager)
        {
            this.manager = notesManager;

        }
    }
}
