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
        ///        "imageString" = "+kxqtj7TY/PivLQAVx3xJh3aNazd47g2S8UYCjgmpxOOFPqRxpFLqVlHSBDY6fb6bbiG2jCKOp7k+pPc1aoor1lFRVkcjberCiiimIKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKjuLeK6geG4jWSJxhlcZBFSUUbgcJq/hW50pmuNKDXFr1a3Jy6fT+8Pbr9awZYbXVYCsihh05HK16xzWBrfha31Jzc2xFvef31Hyv/vD+vX6152IwSl70NGdVOv0kea2uoaz4IYvp7fa9MJy9pIcpjvt/un6ceoT9KsPl26YzyzHlmPqTVs5rooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooA5nxuVGmQZA3+dwfT5T/8AWrmtOtY7zxLpQlUMskc0bg91MZyP5Vr+PZ/9IsYPRXc/oB/Wq3g+H7V4iabGUsrbbn/bkP8ARU/WvKqx58XFLodkdKFzFhgk0vUbjTbgktE20Mf4l7H8Rg1Hcx+XJnsa6nx3pf7uLVoF+eD5JvdCeD+BP5GuelxcWwkHpzWFan7Obj06FQlzpMzLyPfGTXN30WCa6sjKEGsPUYfvcVnTdnY0equZNs+HFbtrJkCuf+5L+Na1lJlRV1VdXFB2NqJ+lXInrMier0L9K4Zo6Ey+hqYGqqPUgeudxGWg9PElVN1L5lLlAt+ZTHmqu8tQSTdeapRuDH3VxhDz2rhNXufNnc5710ep3nl2znPUYFcXcyFnJ9TXp4aFlc56ktbFc5Jx6mug0i26cVjWcRlmHoDXYaVbiNAxHAGauvOysOlG7uW7qZbKyPYkUzStBMh0q/ulO66nd0B/55qBj8ySfoBUVtZy+JfEMGnxlhETulYfwoOp+vb6kV6J4nto7O10yeJAkVpOsZA6KjLs/IHbU0qDdCU+ttAr1UpKCK2nwx3mriOYBkAJ2nvj+n+FdYFCgAAADoAK43TpvI1uAnoZNn58f1FdnXdlySpaHLibqS9BaKKK9A5gooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAOW8XeH7vVprW4sPKMkYKOsjFQQec5wff8AOtPw/oyaJpvkbxJNIxkmkxjc5/oAAB7Ctais1TipOdtSnNuKi9iOeCO4geGZQ8bqVZT3BrzJ7N9H1SfTJySqndEx/iQ9D/T6g16jXOeMNGOo6eLq2XN3a5ZQBy691/r9RWOLo+0hdboujPldnsziJY/LkPoazr+HIJq+9wtzCHB5xUEmJIyO9eO+52LscndR7HNT2UmCBUmoxYJqlC+xxXRvEnZm/E/SrsMlZMUvAq1D
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
                throw;
            }
        }
    }
}