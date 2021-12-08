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
        [HttpDelete]
        [Route("removelable")]
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
    }
}
