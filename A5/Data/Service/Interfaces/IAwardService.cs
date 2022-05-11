using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Models;

namespace A5.Data.Service.Interfaces
{
    public interface IAwardService
    {
        public bool RaiseRequest(Award award);
        public bool ApproveOrReject(Award award,int id, bool result);
        public bool Publish(Award award,int id,string couponCode);
        public bool Reject(Award award, int id);
        public bool Approve(Award award, int id);  
        public IEnumerable<Award> GetawardsByStatus(int statusId);
        public IEnumerable<Award> GetMyAwards(int employeeId,int statusId,Employee employee);
    }
}