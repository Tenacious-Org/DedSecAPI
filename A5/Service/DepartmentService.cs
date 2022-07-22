using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Data.Validations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository.Interface;

namespace A5.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ILogger<EntityBaseRepository<Department>> _logger;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentValidations _departmentValidations;

        public DepartmentService(ILogger<EntityBaseRepository<Department>> logger, IDepartmentRepository departmentRepository, DepartmentValidations departmentValidations)
        {

            _logger = logger;
            _departmentRepository = departmentRepository;
            _departmentValidations = departmentValidations;
        }

        //creates department using department object.
        public bool CreateDepartment(Department department)
        {
            if (department == null) throw new ValidationException("Department should not be null");
            _departmentValidations.CreateValidation(department);
            try
            {
                return _departmentRepository.CreateDepartment(department);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DepartmentService: CreateDepartment(Department department) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentService: CreateDepartment(Department department) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        //updates department using department object.
        public bool UpdateDepartment(Department department)
        {
            if (department == null) throw new ValidationException("Department should not be null");
            _departmentValidations.UpdateValidation(department);
            try
            {
                return _departmentRepository.UpdateDepartment(department);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DepartmentService: UpdateDepartment(Department department) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentService: UpdateDepartment(Department department) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        //Get department using Department Id
        public Department? GetDepartmentById(int departmentId)
        {
            if (departmentId <= 0) throw new ValidationException("Department Id must be greater than zero");
            try
            {
                return _departmentRepository.GetDepartmentById(departmentId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DepartmentService: GetDepartmentById{id}) : (Error:{Message}", departmentId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentService: GetDepartmentById({id}) : (Error:{Message}", departmentId, exception.Message);
                throw;
            }
        }
        //Disable department using department id and employee id.
        public bool DisableDepartment(int departmentId, int userId)
        {
            if (departmentId <= 0) throw new ValidationException("Department Id must be greater than zero");
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _departmentValidations.DisableValidation(userId);
            try
            {
                return _departmentRepository.DisableDepartment(departmentId, userId);

            }
            catch (ValidationException exception)
            {
                _logger.LogError("DepartmentService: DisableDepartment(id:{id},employeeId:{employeeId}) : (Error:{Message}", departmentId, userId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        //Gets the count of employees under department.

        //gets the department count by using department id
        public int GetCount(int departmentId)
        {
            if (departmentId <= 0) throw new ValidationException("Department Id must be greater than zero");
            try{
                 return _departmentRepository.GetCount(departmentId);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("DepartmentService: DisableDepartment(departmentId:{departmentId}) : (Error:{Message}", departmentId,exception.Message);
                throw;
            }
           
        }
        //Gets Department by organisation Id.
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int organisationId)
        {
            if (organisationId <= 0) throw new ValidationException("organisation Id must be greater than zero");
            try
            {
                return _departmentRepository.GetDepartmentsByOrganisationId(organisationId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DepartmentService: GetDepartmentsByOrganisationId({organisationId}) : (Error:{Message}", organisationId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentService: GetDepartmentsByOrganisationId({organisationId}) : (Error:{Message}", organisationId, exception.Message);
                throw;
            }

        }
        //Gets all the department
        public IEnumerable<object> GetAllDepartments()
        {
            var department = _departmentRepository.GetAllDepartment();
            return department.Select(Department => new
            {
                id = Department.Id,
                departmentName = Department.DepartmentName,
                organisationName = Department?.Organisation?.OrganisationName,
                isActive = Department?.IsActive,
                addedBy = Department?.AddedBy,
                addedOn = Department?.AddedOn,
                updatedBy = Department?.UpdatedBy,
                updatedOn = Department?.UpdatedOn
            });

        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }

    }


}

