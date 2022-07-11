<<<<<<< Updated upstream:A5/Service/Interfaces/IAwardTypeService.cs
using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;

namespace A5.Service.Interfaces
{
    public interface IAwardTypeService : IEntityBaseRepository<AwardType>
    {
        public bool CreateAwardType(AwardType awardType);
    }
=======
using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;

namespace A5.Data.Service.Interfaces
{
    public interface IAwardTypeService : IEntityBaseRepository<AwardType>
    {
        public bool CreateAwardType(AwardType awardType);
        public object ErrorMessage(string ValidationMessage);
    }
>>>>>>> Stashed changes:A5/Data/Service/Interfaces/IAwardTypeService.cs
}