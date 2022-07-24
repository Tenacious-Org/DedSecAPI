using A5.Data.Repository.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A5.Models
{
    public class Employee : IEntityBase, IAudit
    {
    
        public int Id { get; set; }
        public string   ACEID { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string  Email { get; set; }
        public byte[]? Image{ get; set; }
        public string? ImageName { get ; set; }
        public string  Gender {get;set;}
        public DateTime  DOB { get; set; }
        public int  OrganisationId { get; set;}
        public int  DepartmentId { get; set;}
        public int DesignationId { get; set; }
        public int ? ReportingPersonId { get; set;}
        public int? HRId { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }= true;
        
        [NotMapped]
        public string ImageString {get;set;}

        //Audit
        public int AddedBy { get; set; }
        public DateTime AddedOn{ get; set; }
        public int ? UpdatedBy { get; set; }
        public DateTime ? UpdatedOn { get; set; }

        //Navigation
        [ForeignKey("DesignationId")]
        public virtual Designation ? Designation{ get; set;}

        [ForeignKey("ReportingPersonId")]
        public virtual Employee? ReportingPerson{ get; set; }

        [ForeignKey("HRId")]
        public virtual Employee? HR{ get; set; }
        
        public virtual ICollection<Employee> ? Reportingpersons { get; set; }
        public virtual ICollection<Employee> ? Hrs { get; set; }
   
       
    }

    public class Login{
        public string ?  Email { get; set; }
        public string ? Password { get; set; }
    }

    public class ForgotPassword{
        public string ?  ACEID { get; set; }
        public string ? Email { get; set; }
    }
}