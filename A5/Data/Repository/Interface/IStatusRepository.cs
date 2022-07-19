using A5.Models;
namespace A5.Data.Repository.Interface
{
    public interface IStatusRepository
    {
        public Status? GetStatusById(int statusId);
        public IEnumerable<Status> GetAllStatus();


    }
    
}