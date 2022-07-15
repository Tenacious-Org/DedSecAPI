using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(ILogger<EmployeeController> logger,EmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary>
        ///  This Method is used to view all Employees
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewEmployees
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return List of Employees.
        /// </returns>

         [HttpGet("GetAll")]
        public ActionResult GetAllEmployees()
        {
            try{
                var data = _employeeService.GetAllEmployees();
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("EmployeeController : GetAllEmployees() : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view single Employee by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleEmployee
        ///     {
        ///        "EmployeeId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns signle Employee by id
        /// </returns>

        [HttpGet("GetById")]
        public ActionResult GetEmployeeById([FromQuery] int id)
        {
            if (id <= 0) return BadRequest("Id cannot be null ");
            try{
                var data = _employeeService.GetEmployeeById(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("EmployeeController : GetEmployeeById(int id) : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to create new employee
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / CreateEmployee
        ///     {
        ///        "ACEID" = "INT0987",
        ///        "FirstName" = "Sanajy",
        ///        "LastName" = "Subramani",
        ///        "Email" = "sanjay@gmail.com",
        ///        "Gender" = "Male",
        ///        "DOB" = "28-03-2002",
        ///        "OrganisationId" = "1",
        ///        "DepartmentId" = "3",
        ///        "DesignationId" = "6",
        ///        "ReportingPersonId" = "2",
        ///        "HRId" = "4",
        ///        "Password" = "SANJAY_SUBRAMANI0987",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="employee">String</param>
        /// <returns>
        ///Return "Employee Added Successfully" when the Employee is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(Employee employee)
        {
            if(employee==null) return BadRequest("Employee should not be null");
            try{
                var data=_employeeService.CreateEmployee(employee,GetCurrentUserId());
                 return  Ok(data); 
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("EmployeeController : Create(Employee employee) : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
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
        /// <param name="employee">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(Employee employee)
        {
            if(employee==null) return BadRequest("Employee should not be null");
            try{
                var data=_employeeService.UpdateEmployee( employee,GetCurrentUserId());
               return  Ok(data); 
                
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("EmployeeController : Update(Employee employee,int id) : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to disable the Employee by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / DisableEmployee
        ///     {
        ///        "EmployeeId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return "Employee Disabled Successfully" message when the isactive filed is set to 0 otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPut("Disable")]
        public ActionResult Disable(int id)
        {
            if (id <= 0) return BadRequest("Id cannot be null ");
            try
            {
       
                var checkEmployee = _employeeService.GetEmployeeCount(id);
                if(checkEmployee>0){
                    return Ok(checkEmployee);
                }else{
                    var data = _employeeService.DisableEmployee(id,GetCurrentUserId());
                    return Ok(data);
                }
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("EmployeeController : Disable(int id) : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view All Employees whose comes under one Department.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewEmployeeByDepartmentId
        ///     {
        ///        "DepartmentId" = "1",    
        ///        "EmployeeId" = "3",
        ///        "EmployeeId" = "4",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns List of Employees from DepartmentId
        /// </returns>

        [HttpGet("GetEmployeeByDepartment")]
        public ActionResult GetEmployeeByDepartmentId(int id)
        {
            if (id <= 0) return BadRequest("Id cannot be null ");
            try
            {
                var data = _employeeService.GetEmployeeByDepartmentId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("EmployeeController : GetEmployeeByDepartmentId(int id) : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        [HttpGet("GetReportingPersonByDepartment")]
        public ActionResult GetReportingPersonByDepartment(int id)
        {
            if (id <= 0) return BadRequest("Id cannot be null ");
            try
            {
                var data = _employeeService.GetReportingPersonByDepartmentId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("EmployeeController : GetReportingPersonbyDepartment(int id) : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        [HttpGet("GetHrByDepartment")]
        public ActionResult GetHrByDepartment(int id)
        {
            if (id <= 0) return BadRequest("Id cannot be null ");
            try
            {
                var data = _employeeService.GetHrByDepartmentId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("EmployeeController : GetHrByDepartment(int id) : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view All Employees whose comes under one Organisation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewEmployeeByOrganisationId
        ///     {
        ///        "OrganisationId" = "1",    
        ///        "EmployeeId" = "3",
        ///        "EmployeeId" = "4",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns List of Employees from OrganisationId
        /// </returns>

        [HttpGet("GetEmloyeeByOrganisation")]
        public ActionResult GetEmployeeByOrganisation(int id)
        {
            if (id <= 0) return BadRequest("Id cannot be null ");
            try
            {
                var data = _employeeService.GetEmployeeByOrganisation(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("EmployeeController : GetEmployeebyOrganisation(int id) : (Error: {exception.Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to view All Employees whose comes under one Requester.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewEmployeeByRequesterId
        ///     {
        ///        "OrganisationId" = "1",    
        ///        "EmployeeId" = "3",
        ///        "EmployeeId" = "4",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns List of Employees from RequesterId
        /// </returns>
        
        [HttpGet("GetEmployeeByRequesterId")]
        public ActionResult GetEmployeeByRequesterId(int id)
        {
            if (id <= 0) return BadRequest("Id cannot be null ");
            try
            {
                var data = _employeeService.GetEmployeeByRequesterId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("EmployeeController : GetEmployeeByRequesterId(int id) : (Error: {Message})",exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
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