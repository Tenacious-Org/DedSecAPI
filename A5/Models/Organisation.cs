using A5.Data.Repository.Interface;

namespace A5.Models
{
    public class Organisation : IEntityBase, IAudit
    {
        public int Id { get; set; }
        public string? OrganisationName { get; set; }
        public bool IsActive { get; set; } = true;

        //Audit
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        //Navigation 
        public virtual ICollection<Department>? Departments { get; set; }
    }
}