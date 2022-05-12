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
    
        public bool Publish(Award award,int id);
         public bool Approve(Award award, int id);
        public bool Reject(Award award, int id);
        public IEnumerable<Award> GetAwardsByStatus(int statusId);
        public IEnumerable<Award> GetMyAwards(int employeeId);
        public IEnumerable<Award> GetRequestedAward(int employeeId);
        public Award GetAward(int id);
        public bool AddComment(Comment comment);
    }
}