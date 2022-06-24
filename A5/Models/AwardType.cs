using A5.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using A5.Models;

namespace A5.Models
{
    public class AwardType : IEntityBase, IAudit
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string AwardName {get;set;}
        [Required]
        public string AwardDescription {get;set;}
        public byte[] ? Image { get ; set; }

        public string ? ImageName { get ; set; }
    
        [Required]
        public bool IsActive {get;set;} = true;
        [Required]
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}

        public int ? UpdatedBy {get; set;}
        public DateTime ? UpdatedOn {get;set;}

        [NotMapped]
        public string ImageString {get;set;}


       
      
        
        
       
    }
}