using A5.Models;
namespace A5.Data.Repository.Interface
{
    public interface IStatusRepository
    {
        Status? GetStatusById(int statusId);
        IEnumerable<Status> GetAllStatus();
    }

}