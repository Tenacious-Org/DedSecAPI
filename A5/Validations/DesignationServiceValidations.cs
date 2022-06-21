using A5.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using A5.Data.Service;
using System.Text.RegularExpressions;
using A5.Data;
namespace A5.Validations
{
    public class DesignationServiceValidations
    {
        private readonly AppDbContext _context;
        public DesignationServiceValidations(AppDbContext context)
        {
            _context=context;
        }

        public bool CreateValidation(Designation designation)
        {           
            if(String.IsNullOrEmpty(designation.DesignationName)) throw new ValidationException("Designation Name should not be null or Empty.");
            if(_context.Designations.Any(nameof=>nameof.DesignationName==designation.DesignationName)) throw new ValidationException("Designation name already exists");           
            if(!( Regex.IsMatch(designation.DesignationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Designation Name should have only alphabets.No special Characters or numbers are allowed");
            if(designation.IsActive == false) throw new ValidationException("Designation should be Active when it is created.");
            if(designation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool UpdateValidation(Designation designation,int id)
        {
            if((id == null)) throw new ValidationException("Designation Id should not be null.");
            if(id!=designation.Id) throw new ValidationException("Designation Id not found.");
            if(string.IsNullOrEmpty(designation.DesignationName)) throw new ValidationException("Designation name should not be null or empty");
            if(_context.Designations.Any(nameof=>nameof.DesignationName==designation.DesignationName)) throw new ValidationException("Designation name already exists");           
            if(!( Regex.IsMatch(designation.DesignationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Designation Name should have only alphabets.No special Characters or numbers are allowed");
            if(designation.IsActive == false) throw new ValidationException("To update designation it should be active");
            if(designation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            if(designation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public bool DisableValidation(int id)
        {
            Designation designation = new Designation();
            if((id == null)) throw new ValidationException("Designation Id should not be null.");
            if(id!=designation.Id) throw new ValidationException("Designation Id not found.");
            if(designation.IsActive==false) throw new ValidationException("Designation is already disabled");
            if(designation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public bool ValidateGetById(int id)
        {
            Designation designation = new Designation();
            if((id == null)) throw new ValidationException("Designation Id should not be null.");
            if(id!=designation.Id) throw new ValidationException("Designation Id not found.");
            else return true;
        }
         
        
        public static void ValidateGetByDepartment(int id)
        {
            if(id==0) throw new ValidationException("Organisation should not be null");

        }
    }
}