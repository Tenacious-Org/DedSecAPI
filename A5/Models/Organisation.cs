using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Models
{
    public class Organisation : IEntityBase, IAudit
    {
        public int Id{ get; set; }
        public string? OrganisationName{ get; set; }
        public bool IsActive{ get; set; } = true;

        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int UpdatedBy {get; set;}
        public DateTime UpdatedOn {get;set;}
         
        //navigation 
        public ICollection<Department> Departments {get;set;}

    }
}