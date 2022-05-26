using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using A5.Data.Repository;

namespace A5.Data.Service
{
    public class EmployeeService : EntityBaseRepository<Employee>, IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        public EmployeeService(AppDbContext context, MasterRepository master) : base(context)
        {
            _context = context;
            _master = master;
        }

        public IEnumerable<Employee> GetByHR(int id)
        {
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.HRId == id && nameof.IsActive == true).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }

        public IEnumerable<Employee> GetByReportingPerson(int id)
        {
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }

        public IEnumerable<Employee> GetEmployeeByDepartmentId(int id)
        {
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
         public IEnumerable<Employee> GetEmployeeByRequesterId(int id)
        {
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Employee> GetEmployeeByOrganisation(int id)
        {
            try
            {
                var result = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.OrganisationId == id).ToList();
                return result;
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
        public IEnumerable<object> GetAllEmployees()
         {
             try
             {
                 var employee = _master.GetAllEmployees();
                    return employee.Select( Employee => new{
                            id = Employee.Id,
                            aCEId = Employee.ACEID,
                            firstName = Employee.FirstName,
                            lastName = Employee.LastName,
                            email = Employee.Email,
                            image = Employee.Image,
                            gender = Employee.Gender,
                            organisationName = Employee.Designation.Department.Organisation.OrganisationName,
                            departmentName = Employee.Designation.Department.DepartmentName,
                            designationName = Employee.Designation.DesignationName,
                            reportingPersonName = Employee.ReportingPerson.FirstName,
                            hRName = Employee.HR.FirstName,
                            password = Employee.Password,
                            isActive = Employee.IsActive,
                            addedBy = Employee.AddedBy,
                            addedOn = Employee.AddedOn,
                            updatedBy = Employee.UpdatedBy,
                            updatedOn = Employee.UpdatedOn
                    });
             }
             catch(Exception exception)
             {
                 throw exception;
             }
            
         }

    }
}