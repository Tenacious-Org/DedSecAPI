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
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<IDepartmentService> _logger;
        private readonly IDepartmentService _departmentService;
        public DepartmentController(ILogger<IDepartmentService> logger,IDepartmentService departmentService)
        {
            _logger= logger;
            _departmentService = departmentService;
        }

        /// <summary>
        ///  This Method is used to view all department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewDepartment
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return List of Departments.
        /// </returns>
       
        [HttpGet("GetAll")]
        public ActionResult GetAllDepartment()
        {
            try
            {
                var result = _departmentService.GetAllDepartments();
                return Ok(result);
            }           
            catch(Exception exception)
            {
                _logger.LogError("DepartmentController : GetAllDepartment() : (Error: {Message})",exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view All departments which are comes under one organisation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewDepartmentsByOrganisationId
        ///     {
        ///        "OrganisationId" = "1",   
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <param name="id">String</param>
        /// <returns>
        ///Returns List of Departments from OrganisationId
        /// </returns>

        [HttpGet("GetDepartmentsByOrganisationId")]
        [AllowAnonymous]
        public ActionResult GetDepartmentsByOrganisationId(int id)
        {
            if (id <= 0) return BadRequest("Organisation Id must be greater than zero");
            try{
                var data = _departmentService.GetDepartmentsByOrganisationId(id);
                return Ok(data);
            }          
            catch(ValidationException exception)
            {
                 _logger.LogError("DepartmentController : GetDepartmentByOrganisationId({id}) : (Error: {Message})",id,exception.Message);
                return BadRequest(_departmentService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                _logger.LogError("DepartmentController : GetDepartmentByOrganisationId({id}) : (Error: {Message})",id,exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view single Departmnet by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleDepartment
        ///     {
        ///        "DepartmentId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns signle Department by id
        /// </returns>

        [HttpGet("GetById")]
        public ActionResult GetDepartmentById(int id)
        {
            if (id <= 0) return BadRequest("Id must be greater than zero");
            try{
                var data = _departmentService.GetDepartmentById(id);
                return Ok(data);
            }           
            catch(ValidationException exception)
            {
                 _logger.LogError("DepartmentController : GetDepartmentById({id}) : (Error: {exception.Message})",id,exception.Message);
                return BadRequest(_departmentService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                  _logger.LogError("DepartmentController : GetDepartmentById({id}) : (Error: {exception.Message})",id,exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to create new department under corresponding organisation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / CreateDepartment
        ///     {
        ///        "departmentName" = "Dotnet",
        ///        "organisationId" = "2",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="department">String</param>
        /// <returns>
        ///Return true when the Department is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(Department department)
        {
            if(department==null) return BadRequest("Department should not be null");
            try{
                department.AddedBy=GetCurrentUserId();
                var data=_departmentService.CreateDepartment(department);
                return data ? Ok("Department created successfully"):BadRequest(); 
            }           
            catch(ValidationException exception)
            {
                 _logger.LogError("DepartmentController : Create(Department department) : (Error: {Message})",exception.Message);
                return BadRequest(_departmentService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                _logger.LogError("DepartmentController : Create(Department department) : (Error: {Message})",exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view single Organisation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / Update
        ///     {
        ///        "OrganisationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="department">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(Department department)
        {
            if(department==null) return BadRequest("Department should not be null");
            try{
                department.UpdatedBy=GetCurrentUserId();
                var data=_departmentService.UpdateDepartment(department);
                return data ? Ok("Department updated succesfully"):BadRequest();
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentController : Update(Department department) : (Error: {Message})",exception.Message);
                return BadRequest(_departmentService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                _logger.LogError("DepartmentController : Update(Department department) : (Error: {Message})",exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to disable the Department by id from OrganisationId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / DisableDepartment
        ///     {
        ///        "DepartmentId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return true message when the isactive filed is set to 0 in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPut("Disable")]
        public ActionResult Disable(int id)
        {
            if (id <= 0) return BadRequest("Department Id must be greater than zero");
            try{
                 var checkEmployee = _departmentService.GetCount(id);
                if(checkEmployee>0){
                    return Ok(checkEmployee);
                }else{
                    var data = _departmentService.DisableDepartment(id,GetCurrentUserId());
                    return data ? Ok("Department disabled succesfully"):BadRequest();
                }
                
            }           
            catch(ValidationException exception)
            {
                 _logger.LogError("DepartmentController : Disable({id}) : (Error: {Message})",id,exception.Message);
                return BadRequest(_departmentService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                _logger.LogError("DepartmentController : Disable({id}) : (Error: {Message})",id,exception.Message);
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
                throw new Exception("Error occured while getting current userId");
            }

        }
    }
}