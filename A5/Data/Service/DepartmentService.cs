using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;

namespace A5.Data.Service
{
    public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
    {
        public DepartmentService(AppDbContext context) : base(context) { }
    }
}