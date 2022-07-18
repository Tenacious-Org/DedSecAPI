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
          
          //to create an awardtype using awardtype object
          public bool CreateAwardType(AwardType awardType)
        {
           
            AwardTypeValidations.CreateValidation(awardType);
            bool NameExists=_context.AwardTypes!.Any(nameof=>nameof.AwardName==awardType.AwardName);
            if(NameExists) throw new ValidationException("Award Name already exists");
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
            AwardTypeValidations.UpdateValidation(awardType);
            bool NameExists=_context.AwardTypes!.Any(nameof=>nameof.AwardName==awardType.AwardName);
            if(NameExists) throw new ValidationException("Award Name already exists");
            try{
                return Update(awardType);
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: UpdateAwardType(AwardType awardType) : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //to disable an awardtype using id and employeeId
        public bool DisableAwardType(int id,int employeeId)
        {
             if (employeeId<=0) throw new ValidationException("currents user id must be greater than 0");
            try{
                return Disable(id,employeeId);
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: DisableAwardType(int id) : (Error:{Message}",exception.Message);
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

        // to get an awardtype by Id using id
         public AwardType? GetAwardTypeById(int id)
        {
            if (id <= 0) throw new ValidationException("Award Id must be greater than 0.");
            try{
                return GetById(id);
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardTypeRepository: GetAwardTypeById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
        }  
          public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }    
    }
}
