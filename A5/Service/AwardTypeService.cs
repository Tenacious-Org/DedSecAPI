
using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository;
using A5.Models;
using A5.Service.Interfaces;
using A5.Service.Validations;

namespace A5.Service
{
    public class AwardTypeService : EntityBaseRepository<AwardType>,IAwardTypeService
    {
        private readonly AppDbContext _context;
        public AwardTypeService(AppDbContext context) : base(context) { 
            _context=context;
        }
          public bool CreateAwardType(AwardType awardType)
        {
           
            if(!AwardTypeValidations.CreateValidation(awardType)) throw new ValidationException("Invalid data");
            bool NameExists=_context.AwardTypes.Any(nameof=>nameof.AwardName==awardType.AwardName);
            if(NameExists) throw new ValidationException("Award Name already exists");
            try{
                return Create(awardType);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        
         public bool UpdateAwardType(AwardType awardType)
        {
          
            if(!AwardTypeValidations.UpdateValidation(awardType)) throw new ValidationException("Invalid data");
            try{
                return Update(awardType);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public bool DisableAwardType(int id)
        {
            AwardType awardType=new AwardType();
            if(!AwardTypeValidations.DisableValidation(id)) throw new ValidationException("Invalid data");
            bool NameExists=_context.AwardTypes.Any(nameof=>nameof.AwardName==awardType.AwardName);
            if(NameExists) throw new ValidationException("Award Name already exists");
            try{
                return Disable(id);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
         public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
       
    }
}