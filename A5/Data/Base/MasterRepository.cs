using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Models;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class MasterRepository
    {
        private AppDbContext _context;
        public MasterRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            try
            {
                var departments = _context.Set<Department>().Include("Organisation").ToList();
                return departments;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Designation> GetAllDesignation()
        {
            try
            {
                var designations = _context.Set<Designation>().Include("Department").ToList();
                return designations;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                var employee = _context.Set<Employee>().Include("Designation.Department.Organisation").Include("Designation.Department").Include("Designation").Include("ReportingPerson").Include("HR").Where(nameof => nameof.ReportingPersonId != null && nameof.HRId != null).ToList();
                return employee;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Award> GetAllAwardsList()
        {
            try
            {
                var award = _context.Set<Award>().Include("Awardee").Include("Awardee.ReportingPerson").Include("Awardee.ReportingPerson.ReportingPerson")
                .Include("Awardee.HR").Include("AwardType").Include("Status").ToList();
                return award;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}