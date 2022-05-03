using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Models
{
    public class Organisation : IEntityBase
    {
        public int Id{ get; set; }
        public string? OrganisationName{ get; set; }
        public bool IsActive{ get; set; } = true;
         
        //navigation 
        public ICollection<Department> Departments {get;set;}

    }
}