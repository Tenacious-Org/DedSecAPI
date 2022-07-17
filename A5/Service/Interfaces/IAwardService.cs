using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data.Repository;
using A5.Models;

namespace A5.Service.Interfaces
{
    public interface IAwardService
    {
        public bool RaiseRequest(Award award,int employeeId);
        public bool Approval(Award award);
        public bool AddComment(Comment comment,int employeeId);
        public object GetAwardById(int id);
        public IEnumerable<object> GetAwardsList(int? pageId, int? employeeId);
        public IEnumerable<object> GetComments(int awardId);
        public object ErrorMessage(string ValidationMessage);
    }
}