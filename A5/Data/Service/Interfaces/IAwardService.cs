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
        public IEnumerable<Award> GetAwards(int ? pageId,int?employeeId);
        public IEnumerable<Award> GetRequestedAward(int employeeId);
        public Award GetAwardById(int id);
        public bool AddComment(Comment comment);
        public IEnumerable<Comment> GetComments(int awardId);
    }
}