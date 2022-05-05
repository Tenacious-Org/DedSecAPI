using A5.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using A5.Models;

namespace A5.Models
{
    public class AwardType : IEntityBase, IAudit, IValidation<AwardType>
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string AwardName {get;set;}
        [Required]
        public string AwardDescription {get;set;}
        [Required]
        public bool IsActive {get;set;}
        [Required]
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        [Required]
        public int UpdatedBy {get; set;}
        public DateTime UpdatedOn {get;set;}


        public bool CreateValidation(AwardType awardType)
        {
            if(awardType == null) throw new ValidationException("Award Type shoulfd not be null.");
            else if(String.IsNullOrEmpty(awardType.AwardName)) throw new ValidationException("Award Name should not be null or Empty.");
            else if(awardType.IsActive == false) throw new ValidationException("Award should be Active when it is created.");
            else if(!(awardType.AddedBy <= 0 && awardType.UpdatedBy <= 0)) throw new ValidationException("User Id Should not be Zero.");
            else return true;
        }
        public bool ValidateGetById(int id)
        {
            if(!(id==null)) throw new ValidationException("Award Id should not be null.");
            else if(id!=Id) throw new ValidationException("Award Id not found.");
            else return true;
        }
         public bool UpdateValidation(AwardType awardType,int id)
        {
            if(!(id==null)) throw new ValidationException("Award Id should not be null.");
            else if(id!=Id) throw new ValidationException("Award Id not found");
            else if(awardType==null) throw new ValidationException("Award types should not be null");
            else if(string.IsNullOrEmpty(awardType.AwardType)) throw new ValidationException("Organisation name should not be null or empty");
             else if(awardType.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero.");
            else if(awardType.UpdatedBy >= 0) throw new ValidationException("User Id Should not be Zero.");
            else return true;
        }
    }
}