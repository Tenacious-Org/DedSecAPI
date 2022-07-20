using A5.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using A5.Data;

namespace A5.Data.Validations
{
    public static class DepartmentValidations
    {

        public  static bool CreateValidation(Department department)
        {
            if(string.IsNullOrWhiteSpace(department.DepartmentName)) throw new ValidationException("Department Name should not be null or Empty.");
            if(!( Regex.IsMatch(department.DepartmentName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Department Name should have only alphabets.No special Characters or numbers are allowed");
            if(department.IsActive == false) throw new ValidationException("Department should be Active when it is created.");
            if(department.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }

         public static bool UpdateValidation(Department department)
        {
             if(string.IsNullOrWhiteSpace(department.DepartmentName)) throw new ValidationException("Department name should not be null or empty");
             if(!( Regex.IsMatch(department.DepartmentName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Department Name should have only alphabets.No special Characters or numbers are allowed");
             if(department.IsActive == false) throw new ValidationException("To Update department it should be active");
             if(department.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
             if(department.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
             else return true;
        }

        
    
    }
}