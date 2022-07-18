
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
                _logger.LogError("Error: {Message}",exception.Message);
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
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        //disables awardtype using awardtype id and current user id
        public bool DisableAwardType(int awardtypeid,int employeeId)
        {
            if (employeeId<=0) throw new ValidationException("currents user id must be greater than 0");

            try{
                return _awardTypeRepository.DisableAwardType(awardtypeid,employeeId);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardTypeService: DisableAwardType(int awardtypeid) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
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
         public AwardType? GetAwardTypeById(int awardtypeid)
        {
            if (awardtypeid <= 0) throw new ValidationException("Award Id must be greater than 0.");
            try{
                return _awardTypeRepository.GetAwardTypeById(awardtypeid);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("AwardTypeService: GetAwardTypeById(int id) : (Error:{Message}",exception.Message);
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