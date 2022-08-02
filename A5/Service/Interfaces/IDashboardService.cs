using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data.Repository;
using A5.Models;

namespace A5.Service.Interfaces
{
    public interface IDashboardService
    {
        public IEnumerable<object> GetAllAwards();
        public IEnumerable<object> GetDashboardDetailsByFilters(int organisationId, int departmentId, int awardId, DateTime start, DateTime end);
    }
}