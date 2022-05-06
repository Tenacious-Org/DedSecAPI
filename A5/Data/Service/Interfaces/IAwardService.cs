using A5.Models;
using A5.Data.Base;


namespace A5.Data.Service.Interfaces
{
    public interface IAwardService
    {
        public bool RaiseRequest(Award award);
        public Award GetById(int id);
        public IEnumerable<Award> GetAllAward();
        public IEnumerable<Award> GetAllAwardRequests();

    }
}