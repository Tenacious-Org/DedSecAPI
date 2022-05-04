using A5.Data.Base;
using System.Collections.Generic;
using System.Linq;
using A5.Models;

namespace A5.Models
{
    public class AwardType : IEntityBase, IAudit
    {
        public int Id {get;set;}

        public string? AwardName {get;set;}
        public string? AwardDescription {get;set;}
        public bool IsActive {get;set;}
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int UpdatedBy {get; set;}
        public DateTime UpdatedOn {get;set;}
    }
}