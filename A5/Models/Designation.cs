using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace A5.Models
{
    public class Designation : IEntityBase, IAudit
    {
        public int Id{ get; set; }
        public string?  DesignationName{ get; set; }
        public bool IsActive{ get; set; } = true;
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int UpdatedBy {get; set;}
        public DateTime UpdatedOn {get;set;}

        //Relation
        public int DepartmentId{ get; set; }

        [ForeignKey("DepartmentID")][NotMapped]
        public virtual Department ? Department{ get; set; }
    
    }
}