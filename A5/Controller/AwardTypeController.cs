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
        //private readonly AppDbContext _context;
        private readonly IAwardTypeService _awardTypeService;
        private readonly ILogger<IAwardTypeService> _logger;
        public AwardTypeController( ILogger<IAwardTypeService> logger,IAwardTypeService awardTypeService)
        {
            _awardTypeService = awardTypeService;
            _logger=logger;
        
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
            try{
                
                var data = _awardTypeService.GetAll();
                 return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"AwardType Controller : GetAll() : (Error:{exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
                //AwardTypeValidations awardTypeValidations=new AwardTypeValidations(_context);
                //awardTypeValidations.ValidateGetById(id);
                var data = _awardTypeService.GetById(id);
                 return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"AwardType Controller : GetById(int id) : (Error:{exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try{
                return _awardTypeService.CreateAwardType(awardType) ?  Ok("Awardtype Created."):Problem("Error occured"); 
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"AwardType Controller : Create(AwardType awardType) : (Error:{exception.Message})");
                return BadRequest(_awardTypeService.ErrorMessage($"{exception.Message}"));
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try{
                 return _awardTypeService.UpdateAwardType(awardType) ?  Ok("Awardtype Updated."):Problem("Error occured"); 
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"AwardType Controller : Update(AwardType awardType,int id) : (Error:{exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try{
                 return _awardTypeService.DisableAwardType(id) ?  Ok("Awardtype Disabled."):Problem("Error occured"); 
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"AwardType Controller : Disable(int id) : (Error:{exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }

    }
}