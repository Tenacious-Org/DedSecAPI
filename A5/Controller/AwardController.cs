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
        [HttpPut("Approve")]
        public ActionResult Approve(Award award,int id){
        
            var data=_awardService.Approve(award,id);
            return Ok(data);
        }
         [HttpPut("Reject")]
        public ActionResult Reject(Award award,int id){
        
            var data=_awardService.Reject(award,id);
            return Ok(data);
        }
       
       
        [HttpPut("Publish")]
        public ActionResult Publish(Award award,int id)
        {
            var data=_awardService.Publish(award,id);
            return Ok(data);
        }
        [HttpGet("GetAwardsByStatus")]
        public ActionResult GetAwardsByStatus(int statusId)
        {
            var data=_awardService.GetAwardsByStatus(statusId);
            return Ok(data);
        }
        [HttpGet("GetMyAwards")]
        public ActionResult GetMyAwards(int employeeId)
        {
            var data=_awardService.GetMyAwards(employeeId);
            return Ok(data);
        }
       [HttpGet("GetRequestedAward")]
       public ActionResult GetRequestedAward(int employeeId)
       {
            var data=_awardService.GetRequestedAward(employeeId);
            return Ok(data);
       }
       [HttpGet("GetAward")]
       public ActionResult GetAward(int id)
       {
           var data=_awardService.GetAward(id);
           return Ok(data);
       }
       [HttpPost("AddComment")]
       public ActionResult AddComment(Comment comment,int awardId,int currentUserId)
       {
           var data=_awardService.AddComment(comment,awardId,currentUserId);
           return Ok();
       }
    }
}