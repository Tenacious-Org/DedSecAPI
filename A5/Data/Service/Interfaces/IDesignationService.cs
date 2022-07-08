using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;

namespace A5.Data.Service.Interfaces
{
    public interface IDesignationService : IEntityBaseRepository<Designation>
    {
         public IEnumerable<Designation> GetDesignationsByDepartmentId(int id);
         public bool CreateDesignation(Designation designation);
         public object ErrorMessage(string ValidationMessage);
          int GetCount(int id);
          public IEnumerable<object> GetAllDesignations();
          public bool UpdateDesignation(Designation designation);

    }
}