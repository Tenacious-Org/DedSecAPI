using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A5.Models
{
    public class Department : IEntityBase
    {
        public int Id{ get; set; }
        public string? DepartmentName{ get; set; }
        public bool IsActive{ get; set; } = true;

        //Relation
        public int OrganisationId{ get; set; }

        [ForeignKey("OrganisationId")][NotMapped]
        public virtual Organisation ? Organisation{ get; set; }

        public ICollection<Designation> Designations {get;set;}

        
 
    }
}