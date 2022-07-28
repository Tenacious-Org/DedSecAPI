using System.Collections.Generic;
using System.Linq;
using A5.Models;
namespace A5.Service.Interfaces
{
    public interface IAwardTypeService
    {
        bool CreateAwardType(AwardType awardType);
        bool DisableAwardType(int awardTypeId, int userId);
        bool UpdateAwardType(AwardType awardType);
        AwardType? GetAwardTypeById(int awardTypeId);
        IEnumerable<AwardType> GetAllAwardType();
        object ErrorMessage(string ValidationMessage);


    }
}