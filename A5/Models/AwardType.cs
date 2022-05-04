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


        public bool Validation(AwardType awardType)
        {
            if(awardType == null) throw new NullReferenceException("Organisation should not be null.");
            

            else return true;
        }
    }
}