using A5.Data.Repository.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A5.Models
{
    public class Department : IEntityBase, IAudit
    {
        public int Id{ get; set; }
        public string ? DepartmentName{ get; set; }
        public int OrganisationId{ get; set; }
        public bool IsActive{ get; set; } = true;

        //Audit
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int ? UpdatedBy {get; set;}
        public DateTime ? UpdatedOn {get;set;}

        //Navigation
        [ForeignKey("OrganisationId")]
        public virtual Organisation ? Organisation{ get; set; }
        public virtual ICollection<Designation> ? Designations { get; set; }


        
        
       
        
        
 
    }
}