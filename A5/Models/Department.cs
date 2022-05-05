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


        public bool CreateValidation(Department department)
        {
            if(department == null) throw new ValidationException("Department should not be null.");
            else if(String.IsNullOrEmpty(department.DepartmentName)) throw new ValidationException("Department Name should not be null or Empty.");
            else if(department.IsActive == false) throw new ValidationException("Department should be Active when it is created.");
            else if(department.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else if(department.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool ValidateGetById(int id)
        {
            if(!(id == null)) throw new ValidationException("Department Id should not be null.");
            else if(id!=Id) throw new ValidationException("Department Id not found.");
            else return true;
        }
         public bool UpdateValidation(Department department,int id)
        {
            if(!(id == null)) throw new ValidationException("Department Id should not be null.");
            else if(id!=Id) throw new ValidationException("Department Id not found");
            else if(department == null) throw new ValidationException("Department should not be null");
            else if(string.IsNullOrEmpty(department.DepartmentName)) throw new ValidationException("Department name should not be null or empty");
             else if(department.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else if(department.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool DisableValidation(Department department,int id)
        {
            if(!(id == null)) throw new ValidationException("Department Id should not be null.");
            else if(id != Id) throw new ValidationException("Department Id not found");
            else if(department==null) throw new ValidationException("Department should not be null");
            else if(department.IsActive==false) throw new ValidationException("Department is already disabled");
            else if(department.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool GetByOrganisationIdValidation(int id)
        {
            if(!(id == null)) throw new ValidationException("Organisation Id should not be null.");
            else if(id != OrganisationId) throw new ValidationException("Organisation Id not found");
            else return true;
        }
 
    }
}