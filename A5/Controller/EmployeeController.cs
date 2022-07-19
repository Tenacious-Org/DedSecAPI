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
        public EmployeeController(ILogger<EmployeeController> logger, EmployeeService employeeService)
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
            try
            {
                var data = _employeeService.GetAllEmployees();
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : GetAllEmployees() : (Error: {Message})", exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeController : GetAllEmployees() : (Error: {Message})", exception.Message);
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
            if (id <= 0) return BadRequest("Id cannot be zero or negative");
            try
            {
                var data = _employeeService.GetEmployeeById(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : GetEmployeeById(id : {id}) : (Error: {Message})", id,exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeController : GetEmployeeById(id : {id}) : (Error: {Message})", id,exception.Message);
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
        ///        "image" = "",
        ///        "imageName" = "",
        ///        "Gender" = "Male",
        ///        "DOB" = "28-03-2002",
        ///        "OrganisationId" = "1",
        ///        "DepartmentId" = "3",
        ///        "DesignationId" = "6",
        ///        "ReportingPersonId" = "2",
        ///        "HRId" = "4",
        ///        "Password" = "SANJAY_SUBRAMANI0987",
        ///        "imageString" = "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",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="employee">String</param>
        /// <returns>
        ///Return true when the Employee is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(Employee employee)
        {
            if (employee == null) return BadRequest("Employee should not be null");
            try
            {
                employee.AddedBy=GetCurrentUserId();
                var data = _employeeService.CreateEmployee(employee);
                return data ? Ok(data): BadRequest("Failed to create a new employee");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : Create(Employee employee) : (Error: {Message})", exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {              
                  _logger.LogError("EmployeeController : Create(Employee employee) : (Error: {Message})", exception.Message);
                return Problem(exception.Message);
            }
        }


        /// <summary>
        ///  This Method is used to Update single Employee by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / Update
        ///     {
        ///        "EmployeeId" = "1",
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
            if (employee == null) return BadRequest("Employee should not be null");
            try
            {
                employee.UpdatedBy=GetCurrentUserId();
                var data = _employeeService.UpdateEmployee(employee);
                return data ? Ok(data) : BadRequest("Failed to update an employee");

            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : Update(Employee employee,int id) : (Error: {Message})", exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {              
                  _logger.LogError("EmployeeController : Update(Employee employee,int id) : (Error: {Message})", exception.Message);
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
            if (id <= 0) return BadRequest("Id cannot be zero or negative ");
            try
            {

                var checkEmployee = _employeeService.GetEmployeeCount(id);
                if (checkEmployee > 0)
                {
                    return Ok(checkEmployee);
                }
                else
                {
                    var data = _employeeService.DisableEmployee(id, GetCurrentUserId());
                    return data ? Ok(data) : BadRequest("Failed to disable employee");
                }
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : Disable(id : {id}) : (Error: {Message})", id,exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {   
                 _logger.LogError("EmployeeController : Disable(id : {id}) : (Error: {Message})", id,exception.Message);
                return Problem(exception.Message);
            }
        }


        /// <summary>
        ///  This Method is used to get all reporting persons under an department.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / GetReportingPersonByDepartment
        ///     {
        ///        "DepartmentId" = "1",    
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="departmentId">String</param>
        /// <returns>
        ///Returns List of Reporting persons from DepartmentId
        /// </returns>

        // [HttpGet("GetEmployeeByDepartment")]
        // public ActionResult GetEmployeeByDepartmentId(int DepartmentId)
        // {
        //     if (DepartmentId <= 0) return BadRequest("Id cannot be zero or negative");
        //     try
        //     {
        //         var data = _employeeService.GetEmployeeByDepartmentId(DepartmentId);
        //         return Ok(data);
        //     }
        //     catch (ValidationException exception)
        //     {
        //         _logger.LogError("EmployeeController : GetEmployeeByDepartmentId(id : {id}) : (Error: {Message})",DepartmentId, exception.Message);
        //         return BadRequest(_employeeService.ErrorMessage(exception.Message));
        //     }
        //     catch (Exception exception)
        //     {
        //          _logger.LogError("EmployeeController : GetEmployeeByDepartmentId(id : {id}) : (Error: {Message})",DepartmentId, exception.Message);
        //         return Problem(exception.Message);
        //     }
        // }
        [HttpGet("GetReportingPersonByDepartment")]
        public ActionResult GetReportingPersonByDepartment(int departmentId)
        {
            if (departmentId <= 0) return BadRequest("Id cannot be zero or negative");
            try
            {
                var data = _employeeService.GetReportingPersonByDepartmentId(departmentId);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : GetReportingPersonbyDepartment(id : {id}) : (Error: {Message})",departmentId, exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                 _logger.LogError("EmployeeController : GetReportingPersonbyDepartment(id : {id}) : (Error: {Message})",departmentId, exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to get all HR under an department.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / GetHrByDepartment
        ///     {
        ///        "DepartmentId" = "1",    
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="departmentId">String</param>
        /// <returns>
        ///Returns List of HR persons from DepartmentId
        /// </returns>

        [HttpGet("GetHrByDepartment")]
        public ActionResult GetHrByDepartment(int departmentId)
        {
            if (departmentId <= 0) return BadRequest("Id cannot be zero or negative");
            try
            {
                var data = _employeeService.GetHrByDepartmentId(departmentId);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : GetHrByDepartment(id : {id}) : (Error: {Message})", departmentId,exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeController : GetHrByDepartment(id : {id}) : (Error: {Message})", departmentId,exception.Message);
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
        ///        "OrganisationId" = "2",   
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="organisationId">String</param>
        /// <returns>
        ///Returns List of Employees from OrganisationId
        /// </returns>

        [HttpGet("GetEmloyeeByOrganisation")]
        public ActionResult GetEmployeeByOrganisation(int organisationId)
        {
            if (organisationId <= 0) return BadRequest("Id cannot be zero or negative");
            try
            {
                var data = _employeeService.GetEmployeeByOrganisation(organisationId);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : GetEmployeebyOrganisation(id : {id}) : (Error: {exception.Message})",organisationId, exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                 _logger.LogError("EmployeeController : GetEmployeebyOrganisation(id : {id}) : (Error: {exception.Message})",organisationId, exception.Message);
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
        ///        "RequesterId" = "2",  
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="RequesterId">String</param>
        /// <returns>
        ///Returns List of Employees from RequesterId
        /// </returns>

        [HttpGet("GetEmployeeByRequesterId")]
        public ActionResult GetEmployeeByRequesterId(int RequesterId)
        {
            if (RequesterId <= 0) return BadRequest("Id cannot be zero or negative");
            try
            {
                var data = _employeeService.GetEmployeeByRequesterId(RequesterId);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeController : GetEmployeeByRequesterId(id : {id}) : (Error: {Message})",RequesterId, exception.Message);
                return BadRequest(_employeeService.ErrorMessage(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeController : GetEmployeeByRequesterId(id : {id}) : (Error: {Message})",RequesterId, exception.Message);
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