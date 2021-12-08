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
        public async Task<IActionResult> AddLable([FromBody] LabelModel label
            )
        {
            try
            {
                string result = await this.labelmanager.Lable(label);

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
        [Route("api/Adduserslabel")]
        public async Task<IActionResult> AddLabelbyUserId([FromBody] LabelModel label
            )
        {
            try
            {
                string result = await this.labelmanager.Lable(label);

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
    }
}
