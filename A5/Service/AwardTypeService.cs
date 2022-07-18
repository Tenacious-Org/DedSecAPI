
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
         
         
         //creates awardtype using awardtype object
          public bool CreateAwardType(AwardType awardType)
        {
           
            AwardTypeValidations.CreateValidation(awardType);
            try{
                awardType.Image = System.Convert.FromBase64String(awardType.ImageString!);
                return _awardTypeRepository.CreateAwardType(awardType);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("AwardTypeService: CreateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeService: CreateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
        }
        
        //updates awardtype using awardtype object
         public bool UpdateAwardType(AwardType awardType)
        {
          
            AwardTypeValidations.UpdateValidation(awardType);
            try{
                awardType.Image = System.Convert.FromBase64String(awardType.ImageString!);
                return _awardTypeRepository.UpdateAwardType(awardType);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("AwardTypeService: UpdateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeService: UpdateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //disables awardtype using awardtype id and current user id
        public bool DisableAwardType(int id,int employeeId)
        {
            if (employeeId<=0) throw new ValidationException("current user id must be greater than 0");
            if(id<=0) throw new ValidationException("Award type id should not be zero or negative");
            try{
                return _awardTypeRepository.DisableAwardType(id,employeeId);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardTypeService: DisableAwardType(awardTypeId : {awardTypeId},employeeId : {employeeId}) : (Error:{Message}",id,employeeId,exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeService: DisableAwardType(awardTypeId : {awardTypeId},employeeId : {employeeId}) : (Error:{Message}",id,employeeId,exception.Message);
                throw;
            }
        }

        //gets list of all awardtypes
         public IEnumerable<AwardType> GetAllAwardType()
        {
            
            try{
                return _awardTypeRepository.GetAllAwardTypes();
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: GetAllAwardType() : (Error:{Message}",exception.Message);
                throw;
            }
        }


        //gets awardtype by awardtype Id
         public AwardType? GetAwardTypeById(int awardTypeId)
        {
            if (awardTypeId <= 0) throw new ValidationException("Award Id must be greater than 0.");
            try{
                return _awardTypeRepository.GetAwardTypeById(awardTypeId);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("AwardTypeService: GetAwardTypeById(id : {id}) : (Error:{Message}",awardTypeId,exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeService: GetAwardTypeById(id : {id}) : (Error:{Message}",awardTypeId,exception.Message);
                throw;
            }
        }
         public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
       
    }
}