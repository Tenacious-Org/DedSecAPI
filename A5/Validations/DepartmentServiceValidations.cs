using A5.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using A5.Data.Repository;
using A5.Validations;
using A5.Data;

namespace A5.Validations
{
    public class DepartmentServiceValidations
    {
        // private readonly AppDbContext _context;
        // public DepartmentServiceValidations(AppDbContext context) 
        // {
        //     _context = context;
        // }
        public static void CreateValidation(Department department)
        {
           if(string.IsNullOrEmpty(department.DepartmentName)) throw new ValidationException("Department Name should not be null or Empty.");
            if(department.IsActive == false) throw new ValidationException("Department should be Active when it is created.");
            if(department.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");

        }

        internal static void CreateValidation<T>(T entity) where T : class, IAudit, IEntityBase, new()
        {
            throw new NotImplementedException();
        }

        public static void ValidateGetByOrganisation(int id)
        {
            if(id==0) throw new ValidationException("Organisation should not be null");

        }
    }
}