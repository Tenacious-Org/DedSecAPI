using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        
        private readonly AwardService _awardService;

        public AwardController(AwardService awardService)
        {
            _awardService=awardService;
        }
        [HttpPost("RaiseRequest")]
        public ActionResult RaiseRequest(Award award)
        {
            var data=_awardService.RaiseRequest(award);
            return Ok(data);
        }
        [HttpPut("ApproveOrReject")]
        public ActionResult ApproveOrReject(Award award,int id,bool result)
        {
            var data=_awardService.ApproveOrReject(award,id,result);
            return Ok(data);
        }
        [HttpPut("Publish")]
        public ActionResult Publish(Award award,int id,string couponCode)
        {
            var data=_awardService.Publish(award,id,couponCode);
            return Ok(data);
        }
        [HttpGet("GetAwardsByStatus")]
        public ActionResult GetAwardsByStatus(Award award,int statusId)
        {
            var data=_awardService.Publish(award,statusId);
            return Ok(data);
        }
        [HttpGet("GetMyAwards")]
        public ActionResult GetMyAwards(int employeeId,int statusId)
        {
            var data=_awardService.GetMyAwards(employeeId,statusID);
            return Ok(data);
        }
       
    }
}