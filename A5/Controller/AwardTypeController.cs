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
            try
            {

                var data = _awardTypeService.GetAwardTypeById(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : GetById(int id) : (Error:{Message})", exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
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
        ///        "AwardName" = "RoleStar",
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
            try
            {
                var data = _awardTypeService.CreateAwardType(awardType);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : Create(AwardType awardType) : (Error:{Message})", exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
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
        ///        "OrganisationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="awardType">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(AwardType awardType)
        {
            try
            {
                var data = _awardTypeService.UpdateAwardType(awardType);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : Update(AwardType awardType,int id) : (Error:{Message})", exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
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
            try
            {
                var data = _awardTypeService.DisableAwardType(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeController : Disable(int id) : (Error:{Message})", exception.Message);
                return BadRequest(_awardTypeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

    }
}