using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Models
{
    public class Comment
    {
        public int Id {get; set;}
        public int EmployeeId {get;set;}
        public int AwardId {get; set;}
        public string Comments {get;set;}

        [ForeignKey("AwardId")]
        public virtual Award Award {get;set;}

    }
}