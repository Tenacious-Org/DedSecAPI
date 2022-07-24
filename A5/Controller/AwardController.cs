using Microsoft.AspNetCore.Mvc;
using A5.Models;
using System.ComponentModel.DataAnnotations;
using A5.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly ILogger<IAwardService> _logger;
        private readonly IAwardService _awardService;
        public AwardController(ILogger<IAwardService> logger, IAwardService awardService)
        {
            _awardService = awardService;
            _logger = logger;
        }

        /// <summary>
        ///  This Method is used to raise request for Awardee
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / RaiseRequest
        ///     {
        ///        "requesterId": 11,
        ///        "awardeeId": 8,
        ///        "awardTypeId": 2,
        ///        "reason": "Best Performer",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="award">String</param>
        /// <returns>
        ///Return true when the request is added in the database otherwise return "sorry internal error is occured"
        /// </returns>

        [HttpPost("RaiseRequest")]
        public ActionResult RaiseRequest(Award award)
        {
            if (award == null) return BadRequest("Award cannot be null");
            try
            {
                var data = _awardService.RaiseRequest(award, GetCurrentUserId());
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardController : RaiseRequest(Award award) : (Error:{Message}", exception.Message);
                return BadRequest(_awardService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardController : RaiseRequest(Award award) : (Error:{Message}", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to Approve or reject the request by Approver
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / Approve or reject by Approver
        ///     {
        ///        "requesterId": 11,
        ///        "awardeeId": 8,
        ///        "awardTypeId": 2,
        ///        "approverId": 5,
        ///        "reason": "Best Performer",
        ///        "statusId": 3,
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="award">String</param>
        /// <returns>
        ///Return the true whether the request getting approved or rejected otherwise it show "sorry internal error occured"  
        /// </returns>

        [HttpPut("Approval")]
        public ActionResult Approval(Award award)
        {
            if (award == null) return BadRequest("award should not be null");
            try
            {
                award.UpdatedBy = GetCurrentUserId();
                var data = _awardService.Approval(award);
                return data ? Ok(data) : BadRequest("Can't approve reuqest");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardController : Approval(Award award) : (Error:{Message}", exception.Message);
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardController : Approval(Award award) : (Error:{Message}", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to get the details about the Specific Awardee by id whether he/she is getting Approved or rejected or published
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / Awardee status by id
        ///     {
        ///        AwardeeId = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return the informations and status about the specific Awardee 
        /// </returns>

        [HttpGet("GetAwardById")]
        [AllowAnonymous]
        public ActionResult GetAwardById(int id)
        {
            if (id <= 0) return BadRequest("Award Id must be greater than zero");

            try
            {
                var data = _awardService.GetAwardById(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardController :GetAwardById(int id) : (Error:{Message}", exception.Message);
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardController :GetAwardById(int id) : (Error:{Message}", exception.Message);
                return Problem(exception.Message);
            }

        }

        /// <summary>
        ///  This Method is used to get all awardees including pending Awardees, Approved Awardees, Rejected Awardees, published Awardees
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / GetAwardsList
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return List of Awardees who are in the status of pending, Approved, rejected, published
        /// </returns>

        [HttpGet("GetAwardsList")]
        [AllowAnonymous]
        public ActionResult GetAwardsList(int pageId = 0)
        {
            if (pageId < 0) return BadRequest("pageId must be greater than zero");
            try
            {
                var data = _awardService.GetAwardsList(pageId, GetCurrentUserId());
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardController : GetAwardsList(int pageId) : (Error:{Message})", exception.Message);
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardController : GetAwardsList(int pageId) : (Error:{Message})", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to Add comments for an Awardee who is getting an award
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / AddComment
        ///     { 
        ///        "comments": "congrats aravinth",
        ///        "employeeId": 6,
        ///        "awardId": 1,
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="comment">String</param>
        /// <returns>
        ///Return 
        /// </returns>

        [HttpPost("AddComment")]
        public ActionResult AddComment(Comment comment)
        {
            if (comment == null) return BadRequest("comment should not be null");
            try
            {
                comment.EmployeeId = GetCurrentUserId();
                var data = _awardService.AddComment(comment);
                return data ? Ok(data) : BadRequest("new comments cannot be added");

            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardController : AddComment(Comment comment) : (Error:{Message}", exception.Message);
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardController : AddComment(Comment comment) : (Error:{Message}", exception.Message);
                return Problem(exception.Message);
            }

        }

        /// <summary>
        ///  This Method is used to get comments of Specific awardee
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / GetComments
        ///     {
        ///        "awardId" = "1" 
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="awardId">String</param>
        /// <returns>
        ///Return the comments and some details of awardee 
        /// </returns>

        [HttpGet("GetComments")]
        [AllowAnonymous]
        public ActionResult GetComments(int awardId)
        {
            if (awardId <= 0) return BadRequest("Award Id must be greater than zero");
            try
            {
                var data = _awardService.GetComments(awardId);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardController : GetComments(int id) : (Error:{Message}", exception.Message);
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardController : GetComments(int id) : (Error:{Message}", exception.Message);
                return Problem(exception.Message);
            }

        }



        private int GetCurrentUserId()
        {
            try
            {
                return Convert.ToInt32(User.FindFirst("UserId")?.Value);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}




















