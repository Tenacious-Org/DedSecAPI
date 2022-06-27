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

       
        //  public  bool ValidateName(Organisation organisation)
        //  {
        //      if(_context.Organisations.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName)) return false;
        //      else return true;
             
        //  }
    }

   
}



