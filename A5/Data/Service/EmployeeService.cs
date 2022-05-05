using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;

namespace A5.Data.Service
{
    public class EmployeeService : EntityBaseRepository<Employee>, IEmployeeService
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetByHR(int id)
        {
            return _context.Set<Employee>().Where(nameof => nameof.HRId == id).ToList();
        }

        public IEnumerable<Employee> GetByReportingPerson(int id)
        {
            return _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id).ToList();
        }
    }
}