using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using A5.Models;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class MasterRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MasterRepository> _logger;
        public MasterRepository(AppDbContext context,ILogger<MasterRepository> logger)
        {
            _context = context;
            _logger=logger;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            try
            {
                var departments = _context.Set<Department>().Where(nameof =>nameof.IsActive == true).Include("Organisation").ToList();
                return departments;
            }
           catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Master Repository: GetAllDepartments() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public IEnumerable<Designation> GetAllDesignation()
        {
            try
            {
                var designations = _context.Set<Designation>().Where(nameof =>nameof.IsActive == true).Include("Department").ToList();
                return designations;
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Master Repository: GetAllDesignation() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                var employee = _context.Set<Employee>()
                    .Include("Designation.Department.Organisation")
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.IsActive == true && nameof.ReportingPersonId!=null&&nameof.HRId!=null)
                    .ToList();
                return employee;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Master Repository: GetAllEmployees() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            
        }

        public IEnumerable<Employee> GetUserDetails()
        {
            try
            {
                var employee = _context.Set<Employee>()
                    .Include("Designation.Department.Organisation")
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.IsActive == true)
                    .ToList();
                return employee;
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Master Repository: GetUserDetails() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
      

        public Employee? GetEmployeeById(int id)
        {
            try
            {
                var employee = _context.Set<Employee>()
                    .Include("Designation.Department.Organisation")
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.IsActive == true)
                    .FirstOrDefault(nameof =>nameof.Id == id);
                return employee;
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Master Repository: GetEmployeeById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        
    }
}