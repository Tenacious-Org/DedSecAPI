using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using A5.Data.Service.Validations;
using Microsoft.EntityFrameworkCore;
using A5.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace A5.Data.Service
{
    public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
    {
        private readonly AppDbContext _context;
      //  private EntityBaseRepository<Organisation> _organisation;

        public DepartmentService(AppDbContext context) : base(context) {
                _context=context;
         } 
         
        public bool CreateDepartment(Department department)
        {
            var obj = new DepartmentServiceValidations(_context);
            if(!obj.CreateValidation(department)) throw new ValidationException("Invalid data");
            bool NameExists=_context.Departments.Any(nameof=>nameof.DepartmentName==department.DepartmentName);
            if(NameExists) throw new ValidationException("Department Name already exists");
            try{
                return Create(department);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public bool UpdateDepartment(Department department)
        {
            if(!DepartmentServiceValidations.UpdateValidation(department)) throw new ValidationException("Invalid Data");
             bool NameExists=_context.Departments.Any(nameof=>nameof.DepartmentName==department.DepartmentName);
            if(NameExists) throw new ValidationException("Department Name already exists");
            try{
                return Update(department);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public Department GetByDepartment(int id)
        {
            var obj = new DepartmentServiceValidations(_context);
            if(!obj.ValidateGetById(id)) throw new ValidationException("Invalid Data");
            try{
                return GetById(id);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public bool DisableDepartment(int id)
        {
            var obj = new DepartmentServiceValidations(_context);
            if(!obj.DisableValidation(id)) throw new ValidationException("Invalid Data");
            
            try
            {
                return Disable(id);

            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public int GetCount(int id)
        {
             var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.DepartmentId == id).ToList().Count();
             return checkEmployee;
        }
    }

   
}

// namespace A5.Data.Service
// {
//     public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
//     {
//         private readonly AppDbContext _context;
//         private readonly MasterRepository _master;
//         public DepartmentService(AppDbContext context, MasterRepository master) : base(context) 
//         {
//             _context = context;
//             _master = master;
//         }
        

//          public IEnumerable<Department> GetDepartmentsByOrganisationId(int id)
//          { 
//             DepartmentServiceValidations.ValidateGetByOrganisation(id);
//             try
//             {
//                 var organisationDetails = _context.Set<Department>().Where(nameof => nameof.OrganisationId == id && nameof.IsActive == true).ToList();
//                 return organisationDetails;
//             }
//             catch(Exception exception)
//             {
//                 throw exception;
//             }
             
//          }
//          public IEnumerable<object> GetAllDepartments()
//          {
//             var department = _master.GetAllDepartments();
//             return department.Select( Department => new{
//                 id = Department.Id,
//                 departmentName = Department.DepartmentName,
//                 organisationName = Department.Organisation.OrganisationName,
//                 isActive = Department.IsActive,
//                 addedBy = Department.AddedBy,
//                 addedOn = Department.AddedOn,
//                 updatedBy = Department.UpdatedBy,
//                 updatedOn = Department.UpdatedOn
//             });
             
//          }
//     }
// }