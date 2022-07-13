using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using A5.Service.Validations;
using A5.Data;

namespace A5.Service
{
    public class OrganisationService : EntityBaseRepository<Organisation>, IOrganisationService
    {
        private readonly AppDbContext _context;
     
       private readonly ILogger<EntityBaseRepository<Organisation>> _logger;

        public OrganisationService(AppDbContext context,ILogger<EntityBaseRepository<Organisation>> logger) : base(context,logger) {
                _context=context;
                _logger=logger;
         } 
         
        public bool CreateOrganisation(Organisation organisation)
        {
            if(!OrganisationServiceValidations.CreateValidation(organisation)) throw new ValidationException("Invalid data");
            bool NameExists=_context.Organisations!.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName);
            if(NameExists) throw new ValidationException("Organisation Name already exists");
            try{
                return Create(organisation);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Organisation Service: CreateOrganisation(Organisation organisation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public bool UpdateOrganisation(Organisation organisation)
        {
            if(!OrganisationServiceValidations.UpdateValidation(organisation)) throw new ValidationException("Invalid Data");
            bool NameExists=_context.Organisations!.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName);
            if(NameExists) throw new ValidationException("Organisation Name already exists");
            try{
                return Update(organisation);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Organisation Service: UpdateOrganisation(Organisation organisation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public Organisation? GetByOrganisation(int id)
        {
            if(!OrganisationServiceValidations.ValidateGetById(id)) throw new ValidationException("Invalid Data");
            try{
                return GetById(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Organisation Service: GetByOrganisation(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public bool DisableOrganisation(int id)
        {
            if(!OrganisationServiceValidations.DisableValidation(id)) throw new ValidationException("Invalid Data");
            
            try
            {
                return Disable(id);

            }
           catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Organisation Service: DisableOrganisation(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public int GetCount(int id)
        {
             var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.OrganisationId == id).Count();
             return checkEmployee;
        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
       
    }

   
}



