using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data;
using A5.Data.Repository;
using A5.Models;

namespace A5.Service
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

        public IEnumerable<object> GetAllByOrgwise(int orgid)
        {
            try
            {
                var orgwise = _master.GetAllbyOrgwise(orgid);
                return orgwise.Select(Award => new{
                    
                    organisation = Award.Awardee.Designation.Department.Organisation.OrganisationName,
                    
                    department = Award.Awardee.Designation.Department.DepartmentName,
                    
                    awardName = Award.AwardType.AwardName,
                });
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public IEnumerable<object> GetAllWinners()
        {
            try
            {
                var winners = _master.GetAllWinners();
                return winners.Select(Award => new{
                    
                    organisation = Award.Awardee.Designation.Department.Organisation.OrganisationName,
                    
                    department = Award.Awardee.Designation.Department.DepartmentName,
                    
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