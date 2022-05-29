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
        public bool Approval(Award award);
        public IEnumerable<Award> GetRequestedAward(int employeeId);
        public bool AddComment(Comment comment);
    }
}