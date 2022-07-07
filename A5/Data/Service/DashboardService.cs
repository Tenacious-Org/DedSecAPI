using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data.Repository;
using A5.Models;

namespace A5.Data.Service
{
    public class DashboardService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        public DashboardService(AppDbContext context, MasterRepository master)
        {
            _context = context;
            _master = master;
        }

        public IEnumerable<object> GetAllWinners()
        {
            try
            {
                var winners = _master.GetAllWinners();
                return winners.Select(Award => new{
                    orgid = Award.Awardee.Designation.Department.Organisation.Id,
                    organisation = Award.Awardee.Designation.Department.Organisation.OrganisationName,
                    deptid = Award.Awardee.Designation.Department.Id,
                    department = Award.Awardee.Designation.Department.DepartmentName,
                    awardid = Award.AwardType.Id,
                    awardName = Award.AwardType.AwardName,
                });
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}