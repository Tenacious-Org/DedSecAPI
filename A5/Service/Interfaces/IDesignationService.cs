using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository.Interface;

namespace A5.Service.Interfaces
{
    public interface IDesignationService
    {
        public IEnumerable<Designation> GetDesignationsByDepartmentId(int id);
        public IEnumerable<object> GetAllDesignations();
        public bool CreateDesignation(Designation designation,int employeeId);
        public int GetCount(int id);
        public bool UpdateDesignation(Designation designation,int employeeId);
        public bool DisableDesignation(int id,int employeeId);
        public Designation? GetDesignationById(int id);
        public object ErrorMessage(string ValidationMessage);

    }
}