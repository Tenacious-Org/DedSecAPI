using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A5.Models
{
    public class Department : IEntityBase, IAudit, IValidation<Department>
    {
        public int Id{ get; set; }
        public string? DepartmentName{ get; set; }
        public int OrganisationId{ get; set; }
        public bool IsActive{ get; set; } = true;

        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int UpdatedBy {get; set;}
        public DateTime UpdatedOn {get;set;}

        //Relation
        

        [ForeignKey("OrganisationId")][NotMapped]
        public virtual Organisation ? Organisation{ get; set; }

        public ICollection<Designation> Designations {get;set;}


        public bool Validation(Department department)
        {
            if(department == null) throw new NullReferenceException("Organisation should not be null.");
            

            else return true;
        }

        
 
    }
}