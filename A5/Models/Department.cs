using A5.Data.Base;

namespace A5.Models
{
    public class Department : IEntityBase
    {
        public int Id{ get; set; }
        public string? DepartmentName{ get; set; }
        public bool IsActive{ get; set; } = true;

        //Relation
        public int OrganisationId{ get; set; }
        public Organisation Organisation{ get; set; } = default!;
    }
}