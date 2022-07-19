using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service;
using System.ComponentModel.DataAnnotations;
using A5.Data;
using Microsoft.AspNetCore.Authorization;
using A5.Service.Interfaces;

namespace A5.Controller
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class AwardTypeController : ControllerBase
    {

        private readonly IAwardTypeService _awardTypeService;
        private readonly ILogger<IAwardTypeService> _logger;
        public AwardTypeController(ILogger<IAwardTypeService> logger, IAwardTypeService awardTypeService)
        {
            _awardTypeService = awardTypeService;
            _logger = logger;

        }

        /// <summary>
        ///  This Method is used to view all Award Type
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewAwards
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return List of Awards.
        /// </returns>

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            try
            {
                var data = _awardTypeService.GetAllAwardType();
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : GetAll() : (Error:{Message})", exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeController : GetAll() : (Error:{Message})", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view single Award by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleAward
        ///     {
        ///        "AwardId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns signle Award by id
        /// </returns>

        [HttpGet("GetById")]
        public ActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest("Id cannot be zero or negative");

            try
            {
                var data = _awardTypeService.GetAwardTypeById(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : GetById(id : {id}) : (Error:{Message})", id,exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeController : GetById(id : {id}) : (Error:{Message})", id,exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to create new Award
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / CreateAward
        ///     {
        ///        "awardName": "First Victor",
        ///        "awardDescription": "Good Performer",
        ///        "imageString": "/9j/4AAQSkZJRgABAQEAAAAAAAFxIZJCAmJk/QD3/9PT09PTw4P4V0miXsd7am2kIKigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooonX9Ch1yxML4SZPmikxyp/wPpWtRUyipJpjTad0eG3tlPpd/Jb3MZjkQ4ZT0H09Qeuap3cYkQsK9i8S+G7fX7XBxHcoP3UuP0PqP5da8nvrG40y6ktLyJo5FPQ85HqPUe9eVVouk/I64VFNeZzbgo5FLtNXLy253Clhh8xOlO6tcaunYp/N70mWq+bM+lH2U+lTdFFH5veq8248VrG1OOlQfY979KaaWoNXMxYTVlE6CrDRY6CnQ27O4AByTjgZoc9AjHUsWwCDPpXp3gnwgbTZqupR/wClEfuYmH+qB7n/AGj+g96Z4M8EfYvL1DVI/wB+PmigYf6v/ab39u316d32row+Hs/aTRlWrK3JHYWiiiu85QooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACsXxNpVjqemSG9iYmMfu3Th1Y9gfc4GDxW1WZrxxYp7yp/Osq1lBtlQ3R5jqPhwabCGuZWY4yQorFhls0chWl64wVrufFyZtifavOIv9cR715ainG51p6m0iRS/dz+IxUwsc8gcVHZJnFbUcXyCuGpUlF2RvFJ7mJLbRRA7yR9BmoYxayHYrS5PfaBV/Ukwh+lZVkM3IHvWtNuUbtkSdnoasnhKaazNzat5gHVTwa7fwp4Ig0bZd3oWa96jusX09T7/liiwjFvork90rrB0H0rswCU2+bW2xhWqNJIdRRRXrHKFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFZPiI4sIz6TJWtVa9tI7+1e3mDbHHJU4I9wazqR5oNDi7O5x3iePzLAnHavMR8l0R716Br+hS292bdrjVrhJBmEQyIcjvnI65rg9Y0G805zK1rqcUX96Qp/SvNirNwe51qz1Ru6cM4rcRPkr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"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="awardType">String</param>
        /// <returns>
        ///Return "Award Added Successfully" when the AwardType is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(AwardType awardType)
        {
            if (awardType == null) return BadRequest("AwardType should not be null");

            try
            {
                awardType.AddedBy = GetCurrentUserId();
                var data = _awardTypeService.CreateAwardType(awardType);
                return data ? Ok(data) : BadRequest("Failed to create new Award Type.");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : Create(AwardType awardType) : (Error:{Message})", exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeController : Create(AwardType awardType) : (Error:{Message})", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to update award by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / UpdateAwardType
        ///     {
        ///        "id" : 1;
        ///        "awardName": "First Victor",
        ///        "awardDescription": "Good Performer",
        ///        "imageString": "/9j/4AAQSkZJRgABAQEAAAAAAAFxIZJCAmJk/QD3/9PT09PTw4P4V0miXsd7am2kIKigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooonX9Ch1yxML4SZPmikxyp/wPpWtRUyipJpjTad0eG3tlPpd/Jb3MZjkQ4ZT0H09Qeuap3cYkQsK9i8S+G7fX7XBxHcoP3UuP0PqP5da8nvrG40y6ktLyJo5FPQ85HqPUe9eVVouk/I64VFNeZzbgo5FLtNXLy253Clhh8xOlO6tcaunYp/N70mWq+bM+lH2U+lTdFFH5veq8248VrG1OOlQfY979KaaWoNXMxYTVlE6CrDRY6CnQ27O4AByTjgZoc9AjHUsWwCDPpXp3gnwgbTZqupR/wClEfuYmH+qB7n/AGj+g96Z4M8EfYvL1DVI/wB+PmigYf6v/ab39u316d32row+Hs/aTRlWrK3JHYWiiiu85QooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACsXxNpVjqemSG9iYmMfu3Th1Y9gfc4GDxW1WZrxxYp7yp/Osq1lBtlQ3R5jqPhwabCGuZWY4yQorFhls0chWl64wVrufFyZtifavOIv9cR715ainG51p6m0iRS/dz+IxUwsc8gcVHZJnFbUcXyCuGpUlF2RvFJ7mJLbRRA7yR9BmoYxayHYrS5PfaBV/Ukwh+lZVkM3IHvWtNuUbtkSdnoasnhKaazNzat5gHVTwa7fwp4Ig0bZd3oWa96jusX09T7/liiwjFvork90rrB0H0rswCU2+bW2xhWqNJIdRRRXrHKFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFZPiI4sIz6TJWtVa9tI7+1e3mDbHHJU4I9wazqR5oNDi7O5x3iePzLAnHavMR8l0R716Br+hS292bdrjVrhJBmEQyIcjvnI65rg9Y0G805zK1rqcUX96Qp/SvNirNwe51qz1Ru6cM4rcRPkr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"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="awardType">String</param>
        /// <returns>
        ///Returns the updated data of AwardType
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(AwardType awardType)
        {
            if (awardType == null) return BadRequest("AwardType should not be null");
            try
            {
                awardType.UpdatedBy = GetCurrentUserId();
                var data = _awardTypeService.UpdateAwardType(awardType);
                return data ? Ok(data) : BadRequest("Failed to update award type");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : Update(AwardType awardType) : (Error:{Message})", exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeController : Update(AwardType awardType) : (Error:{Message})", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to disable the Award by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / DisableAward
        ///     {
        ///        "AwardTypeId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return "Award Disabled Successfully" message when the isactive filed is set to 0 otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPut("Disable")]
        public ActionResult Disable(int id)
        {
            if (id <= 0) return BadRequest("Id cannot be zero or negative ");
            try
            {
                var data = _awardTypeService.DisableAwardType(id, GetCurrentUserId());
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : Disable(awardTypeId : {awardTypeId}): (Error:{Message})",id, exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeController : Disable(awardTypeId : {awardTypeId}): (Error:{Message})",id, exception.Message);
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
                throw ;
            }

        }

    }
}