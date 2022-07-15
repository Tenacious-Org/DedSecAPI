using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using A5.Service.Validations;
using A5.Data;
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
         
        public bool CreateOrganisation(Organisation organisation,int employeeId)
        {
            if(!OrganisationServiceValidations.CreateValidation(organisation)) throw new ValidationException("Invalid data");
            try{
                return _organisationRepository.CreateOrganisation(organisation,employeeId);
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
        public bool UpdateOrganisation(Organisation organisation,int employeeId)
        {
            if(!OrganisationServiceValidations.UpdateValidation(organisation)) throw new ValidationException("Invalid Data");
            try{
                return _organisationRepository.UpdateOrganisation(organisation,employeeId);
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
        public Organisation? GetByOrganisation(int id)
        {
            if(!OrganisationServiceValidations.ValidateGetById(id)) throw new ValidationException("Invalid Data");
            try{
                return _organisationRepository.GetByOrganisation(id);
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
            if(!OrganisationServiceValidations.DisableValidation(id)) throw new ValidationException("Invalid Data");
            
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



