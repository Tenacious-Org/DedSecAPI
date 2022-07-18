using A5.Models;
using A5.Data.Repository.Interface;
using A5.Service.Validations;
using System.ComponentModel.DataAnnotations;
using A5.Data.Service.Interfaces;
using A5.Hasher;

namespace A5.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<IEmployeeRepository> _logger;
        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<IEmployeeRepository> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        //gets employee by using department id
        public IEnumerable<Employee> GetEmployeeByDepartmentId(int departmentid)
        {
            
            try
            {
                return _employeeRepository.GetEmployeeByDepartmentId(departmentid);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByDepartmentId(int departmentid) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                 _logger.LogError("EmployeeService: GetEmployeeByDepartmentId(int departmentid) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //Gets reporting person list by Department Id
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int departmentid)
        {
           
            try
            {
                return _employeeRepository.GetReportingPersonByDepartmentId(departmentid);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetReportingPersonByDepartmentId(int departmentid) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetReportingPersonByDepartmentId(int departmentid) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //gets Hr list by department id
        public IEnumerable<Employee> GetHrByDepartmentId(int departmentid)
        {
            
            try
            {
                return _employeeRepository.GetHrByDepartmentId(departmentid);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetHrByDepartmentId(departmentid) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetHrByDepartmentId(departmentid) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //gets list of employees by using requester id
        public IEnumerable<Employee> GetEmployeeByRequesterId(int requesterid)
        {
            
            try
            {
                return _employeeRepository.GetEmployeeByRequesterId(requesterid);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByRequesterId(Designation) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        
        //gets list of employees by using organisation id
        public IEnumerable<Employee> GetEmployeeByOrganisation(int organisationid)
        {
            
            try
            {
                return _employeeRepository.GetEmployeeByOrganisation(organisationid);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByOrganisation(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        
        //gets list of all employees
        public IEnumerable<object> GetAllEmployees()
        {
            try
            {
                var employee = _employeeRepository.GetAllEmployees();
                return employee.Select(employee => GetEmployeeObject(employee));
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetAllEmployees() : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
       
        //gets employee by using employee id
        public object GetEmployeeById(int employeeid)
        {
            
            try
            {
                var employee = _employeeRepository.GetEmployeeById(employeeid);
                if (employee?.HRId == null || employee.ReportingPersonId == null)
                {
                    return new
                    {
                        id = employee?.Id,
                        aceid = employee?.ACEID,
                        firstName = employee?.FirstName,
                        lastName = employee?.LastName,
                        email = employee?.Email,
                        image = employee?.Image,
                        gender = employee?.Gender,
                        dob = employee?.DOB,
                        organisationId = employee?.OrganisationId,
                        departmentId = employee?.DepartmentId,
                        designationId = employee?.DesignationId,
                        organisationName = employee?.Designation?.Department?.Organisation?.OrganisationName,
                        departmentName = employee?.Designation?.Department?.DepartmentName,
                        designationName = employee?.Designation?.DesignationName,
                        password = employee?.Password,
                        isActive = employee?.IsActive,
                        addedBy = employee?.AddedBy,
                        addedOn = employee?.AddedOn,
                        updatedBy = employee?.UpdatedBy,
                        updatedOn = employee?.UpdatedOn
                    };
                }
                else
                {
                    return GetEmployeeObject(employee);
                }

            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeById(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        
        //checks employees by using email and password
        public Employee GetEmployee(string Email, string Password)
        {
            if (Email == null || Password == null) throw new ValidationException("Email or Password cannot be null");
            //Password=PasswordHasher.EncryptPassword(Password);
            try
            {
                return _employeeRepository.GetEmployee(Email, Password);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployee(string Email,string Password) : (Error:{Message})", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        
        //creates employee by using employee object
        public bool CreateEmployee(Employee employee)
        {
            EmployeeServiceValidations.CreateValidation(employee);
            try
            {
                employee.Image = System.Convert.FromBase64String(employee.ImageString!);
                employee.Password = GeneratePassword(employee);
                return _employeeRepository.CreateEmployee(employee);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: CreateEmployee(Employee employee) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        
        //updates employee by using employee object
        public bool UpdateEmployee(Employee employee)
        {
            EmployeeServiceValidations.UpdateValidation(employee);
            try
            {
                employee.Image = System.Convert.FromBase64String(employee.ImageString!);
                return _employeeRepository.UpdateEmployee(employee);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: UpdateEmployee(Employee employee) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        
        //disables employee by using employee id to be disabled and current user id
        public bool DisableEmployee(int id, int employeeId)
        {
            EmployeeServiceValidations.DisableValidation(id);
            try
            {
                return _employeeRepository.DisableEmployee(id, employeeId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: UpdateEmployee(Employee employee) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }
        private object GetEmployeeObject(Employee employee)
        {
            return new
            {
                id = employee.Id,
                aceid = employee.ACEID,
                firstName = employee.FirstName,
                lastName = employee.LastName,
                fullName = employee.FirstName + " " + employee.LastName,
                email = employee.Email,
                image = employee.Image,
                gender = employee.Gender,
                dob = employee.DOB,
                organisationId = employee.OrganisationId,
                departmentId = employee.DepartmentId,
                designationId = employee.DesignationId,
                organisationName = employee?.Designation?.Department?.Organisation?.OrganisationName,
                departmentName = employee?.Designation?.Department?.DepartmentName,
                designationName = employee?.Designation?.DesignationName,
                reportingPersonId = employee?.ReportingPersonId,
                hrId = employee?.HRId,
                reportingPersonName = employee?.ReportingPerson?.FirstName,
                hRName = employee?.HR?.FirstName,
                password = employee?.Password,
                isActive = employee?.IsActive,
                addedBy = employee?.AddedBy,
                addedOn = employee?.AddedOn,
                updatedBy = employee?.UpdatedBy,
                updatedOn = employee?.UpdatedOn
            };
        }
        
        //gets employee count by using employee id
        public int GetEmployeeCount(int id)
        {
            try
            {
                return _employeeRepository.GetEmployeeCount(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeCount(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        private string GeneratePassword(Employee employee)
        {
            var password = employee.FirstName + "@" + employee.ACEID;
            password = PasswordHasher.EncryptPassword(password);
            return password;
        }

         public IEnumerable<Employee> GetByHR(int id)
        {
            EmployeeServiceValidations.ValidateGetByHr(id);
            try
            {
                return _employeeRepository.GetByHR(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetByHR(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetByHR(int id) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        public IEnumerable<Employee> GetByReportingPerson(int id)
        {
            EmployeeServiceValidations.ValidateGetByReportingPerson(id);
            try
            {
                return _employeeRepository.GetByReportingPerson(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetByReportingPerson(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                 _logger.LogError("EmployeeService: GetByReportingPerson(int id) : (Error:{Message}", exception.Message);
                throw;
            }
        }

    }


}