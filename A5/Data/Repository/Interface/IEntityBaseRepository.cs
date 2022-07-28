using System.Collections.Generic;
using System.Linq;

namespace A5.Data.Repository.Interface
{

    public interface IEntityBaseRepository<T> where T : class, IAudit, IEntityBase, new()
    {
        bool Create(T entity);
        bool Disable(int id, int employeeId);

        bool Update(T entity);

        T? GetById(int id);

        IEnumerable<T> GetAll();

    }
}