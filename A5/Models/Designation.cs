using A5.Data.Base;

namespace A5.Models
{
    public class Designation : IEntityBase
    {
        public int Id{ get; set; }
        public string?  DesignationName{ get; set; }
        public bool IsActive{ get; set; } = true;

        //Relation
        public int DepartmentId{ get; set; }
        public Department Department{ get; set; } = default!;
    }
}