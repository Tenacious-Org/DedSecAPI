using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data.Repository;
using A5.Models;
using A5.Data.Repository;

namespace A5.Service.Interfaces
{
    public interface IAwardService
    {
        public bool RaiseRequest(Award award,int id);
        public bool Approval(Award award,int id);
        public bool AddComment(Comment comment);
        public object GetAwardById(int id);
        public IEnumerable<object> GetAwardsList(int ? pageId,int ? employeeId);
        public IEnumerable<object> GetComments(int awardId);
        public object ErrorMessage(string ValidationMessage);
    }
}