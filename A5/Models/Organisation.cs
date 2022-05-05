using A5.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Models
{
    public class Organisation : IEntityBase, IAudit, IValidation<Organisation>
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




        public bool CreateValidation(Organisation organisation)
        {
            if(organisation == null) throw new ValidationException("Organisation should not be null.");
            else if(String.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation Name should not be null or Empty.");
            else if(organisation.IsActive == false) throw new ValidationException("Organisation should be Active when it is created.");
            else if(!(organisation.AddedBy <= 0 && organisation.UpdatedBy <= 0)) throw new ValidationException("User Id Should not be Zero.");
            else return true;
        }

    }
}