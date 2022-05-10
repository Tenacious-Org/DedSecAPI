using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace A5.Models
{
    public class Designation : IEntityBase, IAudit, IValidation<Designation>
    {
        public int Id{ get; set; }
        public string  DesignationName{ get; set; }
        public bool IsActive{ get; set; } = true;
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int ? UpdatedBy {get; set;}
        public DateTime ? UpdatedOn {get;set;}

        //Relation
        public int DepartmentId{ get; set; }

        [ForeignKey("DepartmentID")][NotMapped]
        public virtual Department ? Department{ get; set; }


        public bool CreateValidation(Designation designation)
        {           
            if(String.IsNullOrEmpty(DesignationName)) throw new ValidationException("Designation Name should not be null or Empty.");
            else if(IsActive == false) throw new ValidationException("Designation should be Active when it is created.");
            else if(AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public bool ValidateGetById(int id)
        {
            Designation designation = new Designation();
            if((id == null)) throw new ValidationException("Designation Id should not be null.");
            else if(id!=Id) throw new ValidationException("Designation Id not found.");
            else return true;
        }
         public bool UpdateValidation(Designation designation,int id)
        {
            if((id == null)) throw new ValidationException("Designation Id should not be null.");
            else if(id != Id) throw new ValidationException("Designation Id not found");
            else if(string.IsNullOrEmpty(DesignationName)) throw new ValidationException("Designation name should not be null or empty");
             else if(AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else if(UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
       public bool DisableValidation(Designation designation,int id)
        {
            if(!(id == null)) throw new ValidationException("Designation Id should not be null.");
            else if(id!=Id) throw new ValidationException("Designation Id not found");
            else if(IsActive==false) throw new ValidationException("Designation is already disabled");
            else if(UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool GetByDepartmentIdValidation(int id)
        {
            if(!(id == null)) throw new ValidationException("Department Id should not be null.");
            else if(id != DepartmentId) throw new ValidationException("Department Id not found");
            else return true;
        }
        
    }
}