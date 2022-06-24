using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace A5.Data.Service
{
    public class OrganisationService : EntityBaseRepository<Organisation>, IOrganisationService
    {
        private readonly AppDbContext _context;
        public OrganisationService(AppDbContext context) : base(context) {
                _context=context;
         } 
        public bool CreateValidation(Organisation organisation)
        {
            if(String.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation Name should not be null or Empty.");        
            if(_context.Organisations.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName)) throw new ValidationException("Organisation Name already exists");
            else if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z ]+$"))) throw new ValidationException("Name should have only alphabets.No special Characters or numbers are allowed");
            else if(organisation.IsActive == false) throw new ValidationException("Organisation should be Active when it is created.");
            else if(organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        } 
       
        //  public  bool ValidateName(Organisation organisation)
        //  {
        //      if(_context.Organisations.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName)) return false;
        //      else return true;
             
        //  }
    }

   
}



