using System;
using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service;
using A5.Data;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Route("[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly ILogger<IDesignationService> _logger;
        private readonly IDesignationService _designationService;


        public DesignationController(ILogger<IDesignationService> logger, IDesignationService designationService)
        {
            _logger = logger;
            _designationService = designationService;

        }

        /// <summary>
        ///  This Method is used to view all designation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewDesignation
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return List of Designation.
        /// </returns>

        [HttpGet("GetAll")]
        public ActionResult GetAllDesignation()
        {
            try
            {
                var data = _designationService.GetAllDesignations();
                return Ok(data);
            }
            catch (Exception exception)
            {
                 _logger.LogError("DesignationController : GetAllDesignation() : (Error : {Message})", exception.Message);
                return Problem(exception.Message);
            }

        }

        /// <summary>
        ///  This Method is used to view All designation which are comes under one Department.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewDesignationsByDepartmentId
        ///     { 
        ///        "DesignationId" = "3",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns List of Designations from DepartmentId
        /// </returns>

        [HttpGet("GetDesignationsByDepartmentId")]
        public ActionResult GetDesignationsByDepartmentId(int id)
        {
            if (id <= 0) return BadRequest("Designation Id must be greaer than zero");
            try
            {
                var data = _designationService.GetDesignationsByDepartmentId(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DesignationController : GetDesignationsByDepartmentId(id:{id}) : (Error : Message})",id ,exception.Message);
                return BadRequest(_designationService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("DesignationController : GetDesignationsByDepartmentId(int id) : (Error : Message})",id ,exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view single Designation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleDesignation
        ///     {
        ///        "DesignationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns signle Designation by id
        /// </returns>

        [HttpGet("GetById")]
        public ActionResult GetDesignationById( int id)
        {
            if (id <= 0) return BadRequest("Designation Id must be greater than zero ");
            try
            {
                var data = _designationService.GetDesignationById(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DesignationController : GetDesignationById(int id) : (Error : {Message})", exception.Message);
                return BadRequest(_designationService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("DesignationController : GetDesignationById(int id) : (Error : {Message})", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to create new designation under corresponding department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / CreateDesignation
        ///     {
        ///       "designationName": "Manager",
        ///       "roleId": 3,
        ///       "departmentId": 2,
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="designation">String</param>
        /// <returns>
        ///Return true when the Designation is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(Designation designation)
        {
            if(designation==null) return BadRequest("Designation should not be null");
            try
            {
                designation.AddedBy=GetCurrentUserId();
                var data = _designationService.CreateDesignation(designation);
                return data ? Ok(data):BadRequest("Failed to craete new designation");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DesignationController : Create(Designation designation) : (Error : {Message})", exception.Message);
                return BadRequest(_designationService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("DesignationController : Create(Designation designation) : (Error : {Message})", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to update single Designation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / Update
        ///     {
        ///        "DesignationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="designation">String</param>
        /// <returns>
        ///Returns the updated signle Designation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(Designation designation)
        {
            if(designation==null) return BadRequest("Designation should not be null");
            try
            {
                designation.UpdatedBy=GetCurrentUserId();
                var data = _designationService.UpdateDesignation(designation);
                return data ? Ok(data):BadRequest("Failed to update designation");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("Designation Controller : Update(Designation designation) : (Error : {Message})", exception.Message);
                return BadRequest(_designationService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("Designation Controller : Update(Designation designation) : (Error : {Message})", exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to disable the Designation by id from DepartmentId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / DisableDesignation
        ///     {
        ///        "DesignationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return "Designation Disabled Successfully" message when the isactive filed is set to 0 otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPut("Disable")]
        public ActionResult Disable(int id)
        {
            if (id <= 0) return BadRequest("Designation Id must be greater than zero");
            try
            {

                var checkEmployee = _designationService.GetCount(id);
                if (checkEmployee > 0)
                {
                    return Ok(checkEmployee);
                }
                else
                {
                    var data = _designationService.DisableDesignation(id,GetCurrentUserId());
                   return data ? Ok(data):BadRequest("Failed to disbale designation");
                }

            }
            catch (Exception exception)
            {
                 _logger.LogError("Designation Controller : Disable(int id) : (Error : {Message})", exception.Message);
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