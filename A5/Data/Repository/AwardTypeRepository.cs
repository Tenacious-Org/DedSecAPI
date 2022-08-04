using System.ComponentModel.DataAnnotations;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Data.Validations;

namespace A5.Data.Repository
{
    public class AwardTypeRepository : EntityBaseRepository<AwardType>,IAwardTypeRepository
    {
         private readonly ILogger<EntityBaseRepository<AwardType>> _logger;
         private readonly AwardTypeValidations _awardTypeValidations;
        public AwardTypeRepository(AppDbContext context,ILogger<EntityBaseRepository<AwardType>> logger,AwardTypeValidations awardTypeValidations) : base(context,logger) { 
            _logger=logger;
            _awardTypeValidations=awardTypeValidations;
        }
          
          //to create an awardtype using awardtype object
          public bool CreateAwardType(AwardType awardType)
        {
           
              if (awardType == null) throw new ValidationException("AwardType should not be null");
            _awardTypeValidations.CreateValidation(awardType);
            try{
                return Create(awardType);
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: CreateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
        }
        
        //to update an awardtype using awardtype object
         public bool UpdateAwardType(AwardType awardType)
        {
            if (awardType == null) throw new ValidationException("AwardType should not be null");
            _awardTypeValidations.UpdateValidation(awardType);       
            try{
                return Update(awardType);
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: UpdateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //to disable an awardtype using awardtype id and current user id
        public bool DisableAwardType(int awardTypeId,int userId)
        {
             if (userId <= 0) throw new ValidationException("User id must be greater than Zero");
            if (awardTypeId <= 0) throw new ValidationException("Award type id  must be greater than Zero");
            _awardTypeValidations.DisableValidation(userId);
            try{
                return Disable(awardTypeId,userId);
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: DisableAwardType(awardTypeId : {awardTypeId},employeeId : {employeeId}) : (Error:{Message}",awardTypeId,userId,exception.Message);
                throw;
            }
        }


        //to get all awardtype
        public IEnumerable<AwardType> GetAllAwardTypes()
        {
            try{
                return GetAll();
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: GetAllAwardType() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        // to get an awardtype by Id using  award id
         public AwardType? GetAwardTypeById(int id)
        {
            if (id <= 0) throw new ValidationException("Award Id must be greater than 0.");
            try{
                return GetById(id);
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: GetAwardTypeById(id :{id}) : (Error:{Message}",id,exception.Message);
                throw;
            }
        }    
    }
}
