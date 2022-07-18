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
        //Gets Employee of particular HR by using  HR id.
        public IEnumerable<Employee> GetByHR(int id)
        {
           if(id<=0) throw new ValidationException("HR id should not be null or negative");
            try
            {
                return _employeeRepository.GetEmployeeByDepartmentId(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetByHR(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetByHR(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
        }
       //Gets Employee of particular reporting person by using reporting person id.
        public IEnumerable<Employee> GetByReportingPerson(int id)
        {
            if(id<=0) throw new ValidationException("Reporting person id should not be null or negative");
            try
            {
                return _employeeRepository.GetReportingPersonByDepartmentId(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetByReportingPerson(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetByReportingPerson(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
        }
        //Gets Employee of particular department using department id.
        public IEnumerable<Employee> GetEmployeeByDepartmentId(int id)
        {
            if(id<=0) throw new ValidationException("Department id should not be null or negative");
            try
            {
                return _employeeRepository.GetEmployeeByDepartmentId(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByDepartmentId(id : {id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByDepartmentId(id : {id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
        }
        //Gets reporting Person of particular department by using department id.
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int id)
        {
            if(id<=0) throw new ValidationException("Reporting Person Id should not be null or negative");
            try
            {
                return _employeeRepository.GetReportingPersonByDepartmentId(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetReportingPersonByDepartmentId(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetReportingPersonByDepartmentId(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
        }
        //Gets Hr of particular department using Department Id.
        public IEnumerable<Employee> GetHrByDepartmentId(int id)
        {
            if(id<=0) throw new ValidationException("Department Id should not be null or negative");
            try
            {
                return _employeeRepository.GetHrByDepartmentId(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetHrByDepartmentId(id : {id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetHrByDepartmentId(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
        }
        //Get Employee by using Requester Id.
        public IEnumerable<Employee> GetEmployeeByRequesterId(int id)
        {
           if(id<=0) throw new ValidationException("Requester Id should not be null or negative");
            try
            {
                return _employeeRepository.GetEmployeeByRequesterId(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByRequesterId(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByRequesterId(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
        }
        //Get employees of particular organisation by using organisation id.
        public IEnumerable<Employee> GetEmployeeByOrganisation(int id)
        {
            if(id<=0) throw new ValidationException("Organisation Id should not be null or negative");
            try
            {
                return _employeeRepository.GetEmployeeByOrganisation(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByOrganisation(id : {id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByOrganisation(id : {id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
        }
        //Gets all employees.
        public IEnumerable<object> GetAllEmployees()
        {
            try
            {
                var employee = _employeeRepository.GetAllEmployees();
                return employee.Select(employee => GetEmployeeObject(employee));
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetAllEmployees() : (Error:{Message}", exception.Message);
                throw;
            }
        }
        //Gets Details of Particular employee using employee id.
        public object GetEmployeeById(int id)
        {
           if(id<=0) throw new ValidationException("Id should not be null or negative");
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);
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
                _logger.LogError("EmployeeService: GetEmployeeById(Id:{id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeById(Id:{id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
        }
        //Get employee using email and password.
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
                _logger.LogError("EmployeeService: GetEmployee(Email : {Email},Password : {Password}) : (Error:{Message})", Email,Password,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: GetEmployee(Email : {Email},Password : {Password}) : (Error:{Message})", Email,Password,exception.Message);
                throw;
            }
        }
        //Creates Employee using employee object.
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
                _logger.LogError("EmployeeService: CreateEmployee(Employee employee) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        //Update Employee using employeee object.
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
        //Disable employee by using employee id and current user id.
        public bool DisableEmployee(int id, int employeeId)
        {
            EmployeeServiceValidations.DisableValidation(id);
            if(id<=0) throw new ValidationException("Id should not be null or negative");
            if(employeeId<=0) throw new ValidationException("Current user Id should not be null or negative");
            try
            {
                return _employeeRepository.DisableEmployee(id, employeeId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: DisableEmployee(id :{id},employeeId : {employeeId}) : (Error:{Message}",id,employeeId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeService: DisableEmployee(id :{id},employeeId : {employeeId}) : (Error:{Message}",id,employeeId, exception.Message);
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
            if(id<=0) throw new ValidationException("Employee Id should not be null or negative");
            try
            {
                return _employeeRepository.GetEmployeeCount(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeCount(id:{id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
               _logger.LogError("EmployeeService: GetEmployeeCount(id:{id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
        }
        private string GeneratePassword(Employee employee)
        {
            var password = employee.FirstName + "@" + employee.ACEID;
            password = PasswordHasher.EncryptPassword(password);
            return password;
        }

    }


}