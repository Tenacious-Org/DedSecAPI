using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Service.Validations;

namespace A5.Data.Repository
{
     public class AwardTypeRepository : EntityBaseRepository<AwardType>,IAwardTypeRepository
    {
        private readonly AppDbContext _context;
         private readonly ILogger<EntityBaseRepository<AwardType>> _logger;
        public AwardTypeRepository(AppDbContext context,ILogger<EntityBaseRepository<AwardType>> logger ) : base(context,logger) { 
            _context=context;
            _logger=logger;
        }
          public bool CreateAwardType(AwardType awardType)
        {
           
            if(!AwardTypeValidations.CreateValidation(awardType)) throw new ValidationException("Invalid data");
            bool NameExists=_context.AwardTypes!.Any(nameof=>nameof.AwardName==awardType.AwardName);
            if(NameExists) throw new ValidationException("Award Name already exists");
            try{
                return Create(awardType);
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
                return Update(awardType);
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
            AwardType awardType=new AwardType();
            if(!AwardTypeValidations.DisableValidation(id)) throw new ValidationException("Invalid data");
            bool NameExists=_context.AwardTypes!.Any(nameof=>nameof.AwardName==awardType.AwardName);
            if(NameExists) throw new ValidationException("Award Name already exists");
            try{
                return Disable(id);
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

        public IEnumerable<AwardType> GetAllAwardTypes()
        {
            
            try{
                return GetAll();
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
                return GetById(id);
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
