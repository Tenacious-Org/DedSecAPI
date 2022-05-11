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
        public byte[] ? Image { get ; set; }
       
        [Required]
        public bool IsActive {get;set;} = true;
        [Required]
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}

        public int ? UpdatedBy {get; set;}
        public DateTime ? UpdatedOn {get;set;}


        public bool CreateValidation(AwardType awardType)
        {  
           if(String.IsNullOrEmpty(awardType.AwardName)) throw new ValidationException("Award Name should not be null or Empty.");
            else if(awardType.IsActive == false) throw new ValidationException("Award should be Active when it is created.");
            else if(awardType.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero."); 
            else return true;
        }
        public bool ValidateGetById(int id)
        {
            AwardType awardType=new AwardType();
            if(!(id==null)) throw new ValidationException("Award Id should not be null.");
            else if(id!=Id) throw new ValidationException("Award Id not found.");
            else return true;
        }
         public bool UpdateValidation(AwardType awardType,int id)
        {
            if(string.IsNullOrEmpty(awardType.AwardName)) throw new ValidationException("Organisation name should not be null or empty");
            else if(awardType.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else if(awardType.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool DisableValidation(AwardType awardType,int id)
        {
            if(awardType.IsActive==false) throw new ValidationException("AwardType is already disabled");
            else if(awardType.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        
       
    }
}