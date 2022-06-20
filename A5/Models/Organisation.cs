using A5.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using A5.Data.Service;
using A5.Data;

namespace A5.Models
{
    public class Organisation : IEntityBase, IAudit
    {
        private readonly AppDbContext context;

        public int Id{ get; set; }
        public string OrganisationName{ get; set; }
        public bool IsActive{ get; set; } = true;

        //Audit
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int ? UpdatedBy {get; set;}
        public DateTime ? UpdatedOn {get;set;}
         
        //Navigation 
        public virtual ICollection<Department> ? Departments {get;set;}

        
         public bool ValidateGetById(int id)
        {
            Organisation organisation = new Organisation();
            if((id == null)) throw new ValidationException("Organisation Id should not be null.");
            else return true;
        }
        public bool UpdateValidation(Organisation organisation,int id)
        {
            if(string.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation name should not be null or empty");
            else if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Namse should have only alphabets.No special Characters or numbers are allowed");
            else if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }

        public bool DisableValidation(Organisation organisation,int id)
        {
            if(id <= 0) throw new ValidationException("Organisation Id must be greater than Zero.");
            else if(organisation.IsActive == false) throw new ValidationException("Organisation is already disabled");
            else if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        

        
    }
}