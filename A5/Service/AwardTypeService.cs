
using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository.Interface;
using A5.Data.Repository;
using A5.Models;
using A5.Service.Interfaces;
using A5.Service.Validations;

namespace A5.Service
{
    public class AwardTypeService : IAwardTypeService
    {
        private readonly IAwardTypeRepository _awardTypeRepository;
        private readonly ILogger<AwardTypeService> _logger;
        public AwardTypeService(IAwardTypeRepository awardTypeRepository,ILogger<AwardTypeService> logger )  { 
            _awardTypeRepository=awardTypeRepository;
            _logger=logger;
        }
          public bool CreateAwardType(AwardType awardType)
        {
           
            if(!AwardTypeValidations.CreateValidation(awardType)) throw new ValidationException("Invalid data");
   
            try{
                return _awardTypeRepository.CreateAwardType(awardType);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("AwardType Service: CreateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        
         public bool UpdateAwardType(AwardType awardType)
        {
          
            if(!AwardTypeValidations.UpdateValidation(awardType)) throw new ValidationException("Invalid data");
            try{
                return _awardTypeRepository.UpdateAwardType(awardType);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("AwardType Service: UpdateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public bool DisableAwardType(int id)
        {
            if(!AwardTypeValidations.DisableValidation(id)) throw new ValidationException("Invalid data");
            try{
                return _awardTypeRepository.DisableAwardType(id);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("AwardType Service: DisableAwardType(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

         public IEnumerable<AwardType> GetAllAwardType()
        {
            
            try{
                return _awardTypeRepository.GetAllAwardTypes();
            }
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("AwardTypeRepository: GetAllAwardType() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

         public AwardType? GetAwardTypeById(int id)
        {
          
            try{
                return _awardTypeRepository.GetAwardTypeById(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("AwardTypeService: GetAwardTypeById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
         public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
       
    }
}