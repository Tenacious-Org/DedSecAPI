using A5.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace A5.Models
{
    public class Organisation : IEntityBase, IAudit, IValidation<Organisation>
    {
        public int Id{ get; set; }
        public string? OrganisationName{ get; set; }
        public bool IsActive{ get; set; } = true;

        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int ? UpdatedBy {get; set;}
        public DateTime ? UpdatedOn {get;set;}
         
        //navigation 
        public ICollection<Department> Departments {get;set;}




        public bool CreateValidation(Organisation organisation)
        {
            if(organisation == null) throw new ValidationException("Organisation should not be null.");
            else if(String.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation Name should not be null or Empty.");

            else if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Name should have only alphabets.No special Characters or numbers are allowed");

            else if(organisation.IsActive == false) throw new ValidationException("Organisation should be Active when it is created.");
            else if(organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public bool ValidateGetById(int id)
        {
            Organisation organisation = new Organisation();
            if((id==null)) throw new ValidationException("Organisation Id should not be null.");
            else if(id!=Id) throw new ValidationException("Organisation Id not found.");
            else return true;
        }
        public bool UpdateValidation(Organisation organisation,int id)
        {
            if((id==null)) throw new ValidationException("Organisation Id should not be null.");
            else if(id!=organisation.Id) throw new ValidationException("Organisation Id not found");
            else if(organisation==null) throw new ValidationException("Organisation should not be null");
            else if(string.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation name should not be null or empty");
            else if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Namse should have only alphabets.No special Characters or numbers are allowed");
            else if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }

        public bool DisableValidation(Organisation organisation,int id)
        {
            if(!(id==null)) throw new ValidationException("Organisation Id should not be null.");
            else if(id!=organisation.Id) throw new ValidationException("Organisation Id not found");
            else if(organisation==null) throw new ValidationException("Organisation should not be null");
            else if(organisation.IsActive==false) throw new ValidationException("Organisation is already disabled");
            else if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        

        
    }
}