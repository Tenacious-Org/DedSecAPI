using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository.Interface;

namespace A5.Service.Interfaces
{
    public interface IDesignationService
    {
         IEnumerable<Designation> GetDesignationsByDepartmentId(int departmentId);
         IEnumerable<object> GetAllDesignations();
         bool CreateDesignation(Designation designation);
         int GetCount(int designationId);
         bool UpdateDesignation(Designation designation);
         bool DisableDesignation(int designationId,int userId);
         Designation? GetDesignationById(int designationId);
         object ErrorMessage(string ValidationMessage);

    }
}