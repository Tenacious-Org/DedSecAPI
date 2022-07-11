using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;

namespace A5.Service.Interfaces
{
    public interface IAwardTypeService : IEntityBaseRepository<AwardType>
    {
        public bool CreateAwardType(AwardType awardType);
        public object ErrorMessage(string ValidationMessage);
        public bool DisableAward(int id);
    }
}