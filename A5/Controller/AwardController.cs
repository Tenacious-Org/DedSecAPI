using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service;
using System.ComponentModel.DataAnnotations;
using System.Net;
using A5.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private  readonly ILogger<IAwardService> _logger;
        private readonly IAwardService _awardService;

        public AwardController(ILogger<IAwardService> logger,IAwardService awardService)
        {
            _awardService=awardService;
            _logger=logger;
        }

        /// <summary>
        ///  This Method is used to
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / RaiseRequest
        ///     {
        ///        sample i/p o/p have to write here
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns </response>
        /// <response code="400">If the item is null</response> 
        /// <param name="award">String</param>
        /// <param name="id"></param>
        /// <returns>
        ///Return 
        /// </returns>

        [HttpPost("RaiseRequest")]
        public ActionResult RaiseRequest(Award award,int id)
        {
            if(id<=0) return BadRequest("Id cannot be null or negative");
            try{
                var data=_awardService.RaiseRequest(award,id);
                return Ok(data);
            }           
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("AwardController : RaiseRequest(Award award,int id) : (Error:{Message}",exception.Message);
                return BadRequest(_awardService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        ///  This Method is used to
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /
        ///     {
        ///        sample i/p o/p have to write here
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns </response>
        /// <response code="400">If the item is null</response> 
        /// <param name="award">String</param>
        /// <param name="id"></param>
        /// <returns>
        ///Return 
        /// </returns>

        [HttpPut("Approval")]
        public ActionResult Approval(Award award,int id){
        
            if(id<=0) return BadRequest("Id should not be null or negative");
            try{
                var data=_awardService.Approval(award,id);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                _logger.LogInformation("Award Controller : Approval(Award award,int id) : (Error:{Message}",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                return BadRequest(exception.Message);
            }

        }
        

       
       
        /// <summary>
        ///  This Method is used to
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        ///     {
        ///        sample i/p o/p have to write here
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns </response>
        /// <response code="400">If the item is null</response>
        /// <returns>
        ///Return 
        /// </returns>

       

       /// <summary>
        ///  This Method is used to
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        ///     {
        ///        sample i/p o/p have to write here
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns </response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return 
        /// </returns>

       [HttpGet("GetAwardById")]
       [AllowAnonymous]
       public ActionResult GetAwardById(int id)
       {
        if(id<=0)
            return BadRequest("Id cannot be null or negative");
           
           try{
                var data=_awardService.GetAwardById(id);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                _logger.LogInformation("Award Controller :GetAwardById(int id) : (Error:{Message}",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                _logger.LogError("log: (Error: {Message}",exception.Message);
                return BadRequest(exception.Message);
            }

       }

       /// <summary>
        ///  This Method is used to
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        ///     {
        ///        sample i/p o/p have to write here
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns </response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return 
        /// </returns>

       [HttpGet("GetAwardsList")]
       [AllowAnonymous]
       public ActionResult GetAwardsList(int pageId =0,int employeeId=0)
       {          
           try{
                var data=_awardService.GetAwardsList(pageId,employeeId);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                _logger.LogInformation("Award Controller : GetAwardsList(int pageId,int employeeId) : (Error:{Message})",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                return BadRequest(exception.Message);
            }
       }

        /// <summary>
        ///  This Method is used to 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        sample i/p o/p have to write here
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns </response>
        /// <response code="400">If the item is null</response> 
        /// <param name="comment">String</param>
        /// <returns>
        ///Return 
        /// </returns>

       [HttpPost("AddComment")]
       public ActionResult AddComment(Comment comment)
       {
           if(comment==null) return BadRequest("comment should not be null"); 
           try{
               var data=_awardService.AddComment(comment);
                return Ok(data);               
            }
             catch(ValidationException exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                _logger.LogInformation("Award Controller : AddComment(Comment comment) : (Error:{Message}",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                return BadRequest(exception.Message);
            }

       }

        /// <summary>
        ///  This Method is used to
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        ///     {
        ///        sample i/p o/p have to write here
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns </response>
        /// <response code="400">If the item is null</response> 
        /// <param name="awardId">String</param>
        /// <returns>
        ///Return 
        /// </returns>

       [HttpGet("GetComments")]
       [AllowAnonymous]
       public ActionResult GetComments(int awardId)
       {
           if(awardId<=0) return BadRequest("Award Id should not be negative or zero");
           try{
                var data=_awardService.GetComments(awardId);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                _logger.LogInformation("Award Controller : GetComments(int awardId) : (Error:{Message}",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                _logger.LogError("log: (Error: {Message})",exception.Message);
                return BadRequest(exception.Message);
            }

       }
  
    }
}




















