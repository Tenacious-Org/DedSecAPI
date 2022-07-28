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
         bool RaiseRequest(Award award,int userId);
         bool Approval(Award award);
         bool AddComment(Comment comment);
         object GetAwardById(int awardId);
         IEnumerable<object> GetAwardsList(int? pageId, int? employeeId);
         IEnumerable<object> GetComments(int awardId);
         object ErrorMessage(string ValidationMessage);
    }
}