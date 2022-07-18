using A5.Models;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using A5.Service.Validations;
using A5.Data.Repository.Interface;

namespace A5.Service
{
    public class OrganisationService :  IOrganisationService
    {
        private readonly IOrganisationRepository _organisationRepository;
       private readonly ILogger<OrganisationService> _logger;

        public OrganisationService(ILogger<OrganisationService> logger,IOrganisationRepository organisationRepository)  {
                _logger=logger;
                _organisationRepository=organisationRepository;
         } 
         
        public bool CreateOrganisation(Organisation organisation)
        {
            OrganisationServiceValidations.CreateValidation(organisation);
            try{
                return _organisationRepository.CreateOrganisation(organisation);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("OrganisationService: CreateOrganisation(Organisation organisation) : (Error:{Message}",exception.Message);
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
            OrganisationServiceValidations.UpdateValidation(organisation);
            try{
                return _organisationRepository.UpdateOrganisation(organisation);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("OrganisationService: UpdateOrganisation(Organisation organisation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public Organisation? GetOrganisationById(int id)
        {
            OrganisationServiceValidations.ValidateGetById(id);
            try{
                return _organisationRepository.GetOrganisationById(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("OrganisationService: GetByOrganisation(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
         public IEnumerable<Organisation> GetAllOrganisation()
        {
            
            try{
                return _organisationRepository.GetAllOrganisation();
            }
             catch(ValidationException exception)
            {
                _logger.LogError("OrganisationService: GetAllOrganisation() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public bool DisableOrganisation(int id,int employeeId)
        {
            OrganisationServiceValidations.DisableValidation(id);
            
            try
            {
                return _organisationRepository.DisableOrganisation(id,employeeId);

            }
           catch(ValidationException exception)
            {
               
                _logger.LogError("OrganisationService: DisableOrganisation(int id) : (Error:{Message}",exception.Message);
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
             return _organisationRepository.GetCount(id);
        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
       
    }

   
}



