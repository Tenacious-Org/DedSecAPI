using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A5.Models
{
    public class Employee : IEntityBase, IAudit, IValidation<Employee>
    {
    
        public int Id { get; set; }
        public string  ACEID { get; set; }
        public string ? FirstName { get; set; }
        public string  LastName { get; set; }
        public string  Email { get; set; }
        public byte[] ? Image{ get; set; }
        public string ? ImageName { get ; set; }
        public string Gender {get;set;}
        public DateTime DOB { get; set; }
        public int OrganisationId { get; set;}
        public int DepartmentId { get; set;}
        public int DesignationId { get; set; }
        public int ? ReportingPersonId { get; set;}
        public int? HRId { get; set; }
        public string  Password { get; set; }
        public bool IsActive { get; set; }
        
        [NotMapped]
        public string ImageString {get;set;}

        //Audit
        public int AddedBy { get; set; }
        public DateTime AddedOn{ get; set; }
        public int ? UpdatedBy { get; set; }
        public DateTime ? UpdatedOn { get; set; }

        //Navigation
        [ForeignKey("DesignationId")]
        public virtual Designation? Designation{ get; set;}

        [ForeignKey("ReportingPersonId")]
        public virtual Employee? ReportingPerson{ get; set; }

        [ForeignKey("HRId")]
        public virtual Employee? HR{ get; set; }
        
        public virtual ICollection<Employee> ? Reportingpersons { get; set; }
        public virtual ICollection<Employee> ? Hrs { get; set; }


        public bool CreateValidation(Employee employee)
        {
             if(string.IsNullOrEmpty(employee.FirstName)) throw new ValidationException("Employee name should not be null or empty");
             else if(string.IsNullOrEmpty(employee.LastName)) throw new ValidationException("Employee name should not be null or empty");
             else if(employee.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            return true;
        }
         public bool ValidateGetById(int id)
        {
            Employee employee = new Employee();
            if(!(id==null)) throw new ValidationException("Employee Id should not be null.");
            else if(id != Id) throw new ValidationException("Employee Id not found.");
            return true;
        }
         public bool UpdateValidation(Employee employee,int id)
        {
            if(string.IsNullOrEmpty(employee.FirstName)) throw new ValidationException("Employee name should not be null or empty");
            else if(string.IsNullOrEmpty(employee.LastName)) throw new ValidationException("Employee name should not be null or empty");
            else if(employee.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else if(employee.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool DisableValidation(Employee employee,int id)
        {
            
            if(employee.IsActive == false) throw new ValidationException("Employee is already disabled");
            else if(employee.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        
    }
}