using A5.Models;
using A5.Data;
namespace A5.Service.Interfaces
{
    public interface IStatusService {
                public Status? GetStatusById(int id);
             public IEnumerable<Status> GetAllStatus();

    }
        
    
}