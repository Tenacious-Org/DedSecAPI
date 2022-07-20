using A5.Models;
using A5.Data;

namespace A5.Data.Repository.Interface
{
    public interface IAwardRepository
    {
        public bool RaiseAwardRequest(Award award);
        public bool ApproveRequest(Award award);
        public Award? GetAwardById(int awardId);
        public bool AddComments(Comment comment, int employeeId);
        public IEnumerable<Comment> GetComments(int awardId);
        public IEnumerable<Award> GetAllAwardsList(int? pageId, int? employeeId);

    }
}