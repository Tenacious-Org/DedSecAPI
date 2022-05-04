using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace A5.Models
{
    public class Award : IAudit
    {
        public int Id {get; set;}
        public int RequesterId {get; set;}
        public int AwardeeId {get; set;}
        public int AwardTypeId {get; set;}
        public int ApproverId {get; set;}
        public string Reason {get;set;}
        public string RejectedReason {get;set;}
        public int HRId {get;set;}
        public string CouponCode {get; set;}
        public int StatusId {get;set;}
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int UpdatedBy {get; set;}
        public DateTime UpdatedOn {get;set;}

        [ForeignKey("AwardeeId")]
        public Employee Employee {get;set;}

        [ForeignKey("AwardTypeId")]
        public AwardType AwardType {get;set;}

        [ForeignKey("StatusId")]
        public Status Status {get;set;}

    }
}