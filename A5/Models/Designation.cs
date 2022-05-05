using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace A5.Models
{
    public class Designation : IEntityBase, IAudit, IValidation<Designation>
    {
        public int Id{ get; set; }
        public string?  DesignationName{ get; set; }
        public bool IsActive{ get; set; } = true;
        public int AddedBy {get; set;}
        public DateTime AddedOn{get; set;}
        public int UpdatedBy {get; set;}
        public DateTime UpdatedOn {get;set;}

        //Relation
        public int DepartmentId{ get; set; }

        [ForeignKey("DepartmentID")][NotMapped]
        public virtual Department ? Department{ get; set; }


        public bool CreateValidation(Designation designation)
        {
            if(designation == null) throw new ValidationException("Designation should not be null.");
            else if(String.IsNullOrEmpty(designation.DesignationName)) throw new ValidationException("Designation Name should not be null or Empty.");
            else if(designation.IsActive == false) throw new ValidationException("Designation should be Active when it is created.");
            else if(designation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else if(designation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public bool ValidateGetById(int id)
        {
            if(!(id==null)) throw new ValidationException("Designation Id should not be null.");
            else if(id!=Id) throw new ValidationException("Designation Id not found.");
            else return true;
        }
         public bool UpdateValidation(Designation designation,int id)
        {
            if(!(id==null)) throw new ValidationException("Designation Id should not be null.");
            else if(id!=Id) throw new ValidationException("Designation Id not found");
            else if(designation==null) throw new ValidationException("Designation should not be null");
            else if(string.IsNullOrEmpty(designation.DesignationName)) throw new ValidationException("Designation name should not be null or empty");
             else if(designation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else if(designation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
    }
}