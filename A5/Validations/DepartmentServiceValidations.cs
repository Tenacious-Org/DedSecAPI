using A5.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using A5.Data.Base;
using A5.Validations;
using A5.Data;
using A5.Data.Service.Interfaces;
using System.Text.RegularExpressions;

namespace A5.Validations
{
    public class DepartmentServiceValidations
    {
        private readonly AppDbContext _context;
        public DepartmentServiceValidations(AppDbContext context) 
        {
            _context = context;
        }
        public  bool CreateValidation(Department department)
        {
            if(string.IsNullOrEmpty(department.DepartmentName)) throw new ValidationException("Department Name should not be null or Empty.");
            if(_context.Departments.Any(nameof=>nameof.DepartmentName==department.DepartmentName)) throw new ValidationException("Department name already exists");
            if(!( Regex.IsMatch(department.DepartmentName, @"^[a-zA-Z]+$"))) throw new ValidationException("Department Name should have only alphabets.No special Characters or numbers are allowed");
            if(department.IsActive == false) throw new ValidationException("Department should be Active when it is created.");
            if(department.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }

         public bool UpdateValidation(Department department,int id)
        {
             if(id == 0) throw new ValidationException("Department Id should not be null.");
             if(id!=department.Id) throw new ValidationException("Department Id not found.");
             if(string.IsNullOrEmpty(department.DepartmentName)) throw new ValidationException("Department name should not be null or empty");
             if(_context.Departments.Any(nameof=>nameof.DepartmentName==department.DepartmentName)) throw new ValidationException("Department name already exists");
             if(!( Regex.IsMatch(department.DepartmentName, @"^[a-zA-Z]+$"))) throw new ValidationException("Department Name should have only alphabets.No special Characters or numbers are allowed");
             if(department.IsActive == false) throw new ValidationException("To Update department it should be active");
             if(department.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
             if(department.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
             else return true;
        }
        public bool ValidateGetById(int id)
        {
            Department department=new Department();
            if(id == 0) throw new ValidationException("Department Id should not be null.");
            if(id!=department.Id) throw new ValidationException("Department Id not found.");
            else return true;
        }
        public bool DisableValidation(int id)
        {
             Department department=new Department();
            if(id == 0) throw new ValidationException("Department Id should not be null.");
            if(id!=department.Id) throw new ValidationException("Department Id not found.");
            if(department.IsActive==false) throw new ValidationException("Department is already disabled");
            if(department.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }

        public static void ValidateGetByOrganisation(int id)
        {
            if(id==0) throw new ValidationException("Organisation should not be null");

        }

        
       
    }
}