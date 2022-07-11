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
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly ILogger<IDesignationService> _logger;
        private readonly IDesignationService _designationService;
      

        public DesignationController( ILogger<IDesignationService> logger, IDesignationService designationService)
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
            try{
                var data = _designationService.GetAllDesignations();
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Designation Controller : GetAllDesignation() : (Error : {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
        ///        "DepartmentId" = "1",    
        ///        "DesignationId" = "3",
        ///        "DesignationId" = "4",
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
            try{
                var data = _designationService.GetDesignationsByDepartmentId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Designation Controller : GetDesignationsByDepartmentId(int id) : (Error : {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
        public ActionResult GetByDesignationId([FromQuery] int id)
        {
            
            try{
                // DesignationServiceValidations designationServiceValidations=new DesignationServiceValidations(_context);
                // designationServiceValidations.ValidateGetById(id);
                var data = _designationService.GetById(id);
                return Ok(data);
            }           
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Designation Controller : GetByDesignationId(int id) : (Error : {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
        ///        "DepartmentName" = "Dotnet",
        ///        "DesignationName" = "SSE",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="designation">String</param>
        /// <returns>
        ///Return "Designation Added Successfully" when the Designation is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(Designation designation)
        {
            
            try{
                // DesignationServiceValidations designationValidations=new DesignationServiceValidations(_context);
                // designationValidations.CreateValidation(designation);
                var data = _designationService.CreateDesignation(designation);           
                return Ok(data);
            }         
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Designation Controller : Create(Designation designation) : (Error : {exception.Message})");
                return BadRequest(_designationService.ErrorMessage($"{exception.Message}"));
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }

        /// <summary>
        ///  This Method is used to view single Organisation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleOrganisation
        ///     {
        ///        "OrganisationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="designation">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(Designation designation,int id)
        {
            
            try{
                // DesignationServiceValidations designationServiceValidations=new DesignationServiceValidations(_context);
                // designationServiceValidations.UpdateValidation(designation,id);  
                var data = _designationService.UpdateDesignation(designation);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Designation Controller : Update(Designation designation,int id) : (Error : {exception.Message})");
                return BadRequest(_designationService.ErrorMessage($"{exception.Message}"));
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            
            try
            {
                // DesignationServiceValidations designationServiceValidations=new DesignationServiceValidations(_context);
                // designationServiceValidations.DisableValidation(id);
               // var checkEmployee = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.DesignationId== id).ToList().Count();
                  var checkEmployee = _designationService.GetCount(id);
                if(checkEmployee>0){
                    return Ok(checkEmployee);
                }else{
                    var data = _designationService.Disable(id);
                    return Ok(data);
                }
                
            }           
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Designation Controller : Disable(int id) : (Error : {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }
        
    }
}