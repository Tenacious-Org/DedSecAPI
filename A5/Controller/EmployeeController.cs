using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
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
        private readonly AppDbContext _context;
        public EmployeeController(ILogger<EmployeeController> logger,EmployeeService employeeService,AppDbContext context)
        {
            _logger = logger;
            _employeeService = employeeService;
            _context=context;
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
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Employee Controller : GetAllEmployees() : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try{
                // EmployeeServiceValidations employeeServiceValidations=new EmployeeServiceValidations(_context);
                // employeeServiceValidations.ValidateGetById(id);
                var data = _employeeService.GetEmployeeById(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : GetEmployeeById(int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try{
                // EmployeeServiceValidations employeeServiceValidations=new EmployeeServiceValidations(_context);
                // employeeServiceValidations.CreateValidation(employee);
                employee.Image = System.Convert.FromBase64String(employee.ImageString);              
                var data = _employeeService.Create(employee);
                //employee.Password=_employeeService.GeneratePassword(employee.Id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : Create(Employee employee) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
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
        /// <param name="employee">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(Employee employee,int id)
        {
            try{
                // EmployeeServiceValidations employeeServiceValidations=new EmployeeServiceValidations(_context);
                // employeeServiceValidations.UpdateValidation(employee,id);
                employee.Image = System.Convert.FromBase64String(employee.ImageString);
                var data = _employeeService.Update(employee);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : Update(Employee employee,int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try
            {
                // EmployeeServiceValidations employeeServiceValidations=new EmployeeServiceValidations(_context);
                // employeeServiceValidations.DisableValidation(id);
                var checkEmployee = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.HRId== id || nameof.ReportingPersonId== id  ).ToList().Count();
                if(checkEmployee>0){
                    return Ok(checkEmployee);
                }else{
                    var data = _employeeService.Disable(id);
                    return Ok(data);
                }
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : Disable(int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try
            {
                var data = _employeeService.GetEmployeeByDepartmentId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : GetEmployeeByDepartmentId(int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }
        [HttpGet("GetReportingPersonByDepartment")]
        public ActionResult GetReportingPersonByDepartment(int id)
        {
            try
            {
                var data = _employeeService.GetReportingPersonByDepartmentId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : GetReportingPersonbyDepartment(int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }
        [HttpGet("GetHrByDepartment")]
        public ActionResult GetHrByDepartment(int id)
        {
            try
            {
                var data = _employeeService.GetHrByDepartmentId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : GetHrByDepartment(int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try
            {
                var data = _employeeService.GetEmployeeByOrganisation(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Employee Controller : GetEmployeebyOrganisation(int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
            try
            {
                var data = _employeeService.GetEmployeeByRequesterId(id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : GetEmployeeByRequesterId(int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }
        [HttpPut("GeneratePassword")]
        public ActionResult GeneratePassword(Employee employee,int id)
        {
            try
            {
                var data = _employeeService.GeneratePassword(employee,id);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : GeneratePassword(Employee employee,int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }
        
        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(Employee employee,int id,string Email)
        {
           try
            {
                var data = _employeeService.ChangePassword(employee,id,Email);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Employee Controller : ChangePassword(Employee employee,int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }
        
     
    }
     
}