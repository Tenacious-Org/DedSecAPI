using A5.Models;
using A5.Data;

namespace A5.Data.Repository.Interface
{
    public interface IAwardRepository
    {
        public bool RaiseAwardRequest(Award award, int id);
        public bool ApproveRequest(Award award, int id);
        public Award? GetAwardById(int id);
        public bool AddComments(Comment comment, int employeeId);
        public IEnumerable<Comment> GetComments(int awardId);
        public IEnumerable<Award> GetAllAwardsList(int? pageId, int? employeeId);

    }
}