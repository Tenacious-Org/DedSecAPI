using A5.Models;
namespace A5.Data.Repository.Interface
{
    public interface IStatusRepository
    {
                public Status? GetStatusById(int id);
                        public IEnumerable<Status> GetAllStatus();


    }
    
}