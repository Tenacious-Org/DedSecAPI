using A5.Data.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A5.Models
{
    public class Role
    {
        [Key]
        public int Id {get; set;}
        public string ? RoleName {get;set;}

        public virtual ICollection<Designation> ? Designations {get;set;}
    }
}