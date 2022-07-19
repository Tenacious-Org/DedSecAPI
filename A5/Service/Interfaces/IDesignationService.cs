using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository.Interface;

namespace A5.Service.Interfaces
{
    public interface IDesignationService
    {
        public IEnumerable<Designation> GetDesignationsByDepartmentId(int departmentId);
        public IEnumerable<object> GetAllDesignations();
        public bool CreateDesignation(Designation designation);
        public int GetCount(int designationId);
        public bool UpdateDesignation(Designation designation);
        public bool DisableDesignation(int designationId,int employeeId);
        public Designation? GetDesignationById(int designationId);
        public object ErrorMessage(string ValidationMessage);

    }
}