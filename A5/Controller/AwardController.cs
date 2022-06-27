using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;
using System.Net;
using A5.Data.Service.Interfaces;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly ILogger<IAwardService> _logger;
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
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Award Controller : RaiseRequest(Award award,int id) : (Error:{exception.Message}");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
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
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Award Controller : Approval(Award award,int id) : (Error:{exception.Message}");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
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
        /// <param name="employeeId">String</param>
        /// <returns>
        ///Return 
        /// </returns>

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
        /// <param name="pageId,employeeId">String</param>
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
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Award Controller :GetAwardById(int id) : (Error:{exception.Message}");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
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
       public ActionResult GetAwardsList(int ? pageId,int ? employeeId)
       {          
           try{
                var data=_awardService.GetAwardsList(pageId,employeeId);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Award Controller : GetAwardsList(int pageId,int employeeId) : (Error:{exception.Message}");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
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
           
           try{
               var data=_awardService.AddComment(comment);
                return Ok(data);               
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Award Controller : AddComment(Comment comment) : (Error:{exception.Message}");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
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
       public ActionResult GetComments(int awardId)
       {
           
           try{
                var data=_awardService.GetComments(awardId);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Award Controller : GetComments(int awardId) : (Error:{exception.Message}");
                return BadRequest($"Error : {exception.Message}");
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




















