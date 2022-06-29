using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using A5.Data.Service.Validations;

namespace A5.Data.Service
{
    public class OrganisationService : EntityBaseRepository<Organisation>, IOrganisationService
    {
        private readonly AppDbContext _context;
      //  private EntityBaseRepository<Organisation> _organisation;

        public OrganisationService(AppDbContext context) : base(context) {
                _context=context;
         } 
         
        public bool CreateOrganisation(Organisation organisation)
        {
            if(!OrganisationServiceValidations.CreateValidation(organisation)) throw new ValidationException("Invalid data");
            bool NameExists=_context.Organisations.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName);
            if(NameExists) throw new ValidationException("Organisation Name already exists");
            try{
                return Create(organisation);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public bool UpdateOrganisation(Organisation organisation)
        {
            if(!OrganisationServiceValidations.UpdateValidation(organisation)) throw new ValidationException("Invalid Data");
             bool NameExists=_context.Organisations.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName);
            if(NameExists) throw new ValidationException("Organisation Name already exists");
            try{
                return Update(organisation);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public Organisation GetByOrganisation(int id)
        {
            if(!OrganisationServiceValidations.ValidateGetById(id)) throw new ValidationException("Invalid Data");
            try{
                return GetById(id);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public bool DisableOrganisation(int id)
        {
            if(!OrganisationServiceValidations.DisableValidation(id)) throw new ValidationException("Invalid Data");
            
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
             var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.OrganisationId == id).ToList().Count();
             return checkEmployee;
        }
    }

   
}



