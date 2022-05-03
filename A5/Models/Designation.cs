using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace A5.Models
{
    public class Designation : IEntityBase
    {
        public int Id{ get; set; }
        public string?  DesignationName{ get; set; }
        public bool IsActive{ get; set; } = true;

        //Relation
        public int DepartmentId{ get; set; }

        [ForeignKey("DepartmentID")][NotMapped]
        public virtual Department ? Department{ get; set; }
    
    }
}