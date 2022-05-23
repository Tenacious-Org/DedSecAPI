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
        private readonly ILogger<AwardService> _logger;
        private readonly AwardService _awardService;

        public AwardController(ILogger<AwardService> logger,AwardService awardService)
        {
            _awardService=awardService;
            _logger=logger;
        }
        [HttpPost("RaiseRequest")]
        public ActionResult RaiseRequest(Award award)
        {
            try{
                var data=_awardService.RaiseRequest(award);
                return Ok(data);
            }           
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            
        }
        
         [HttpPut("Approval")]
        public ActionResult Approval(Award award){
        
            
            try{
                var data=_awardService.Approval(award);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

        }
       
        
       [HttpGet("GetRequestedAward")]
       public ActionResult GetRequestedAward(int employeeId)
       {
            
            try{
                var data=_awardService.GetRequestedAward(employeeId);
                return Ok(data);
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

       }
        [HttpGet("GetAwards")]
       public ActionResult GetAwards(int ? pageId,int ? employeeId)
       {
           
           try{
                var data=_awardService.GetAwards(pageId,employeeId);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }

       }

       [HttpGet("GetAwardById")]
       public ActionResult GetAwardById(int id)
       {
           
           try{
                var data=_awardService.GetAwardById(id);
                return Ok(data);
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

       }
       [HttpPost("AddComment")]
       public ActionResult AddComment(Comment comment)
       {
           
           try{
               var data=_awardService.AddComment(comment);
                return Ok(data);               
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

       }
       [HttpGet("GetComments")]
       public ActionResult GetComments(int awardId)
       {
           
           try{
                var data=_awardService.GetComments(awardId);
                return Ok(data);
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

       }
    //    [HttpGet("GetRequestedAwardsList")]
    //    public ActionResult GetRequestedAwardsList(int id)
    //    {
    //        try{
    //             var data=_awardService.GetRequestedAwardsList(id);
    //             return Ok(data);
    //         }
    //         catch(Exception exception)
    //         {
    //             _logger.LogError($"log: (Error: {exception.Message})");
    //             return BadRequest($"Error : {exception.Message}");
    //         }
    //    }

    //    [HttpGet("GetApprovedAwardsList")]
    //    public ActionResult GetApprovedAwardsList(int id)
    //    {
    //        try{
    //             var data=_awardService.GetApprovedAwardsList(id);
    //             return Ok(data);
    //         }
    //         catch(Exception exception)
    //         {
    //             _logger.LogError($"log: (Error: {exception.Message})");
    //             return BadRequest($"Error : {exception.Message}");
    //         }
    //    }
    }
}




















