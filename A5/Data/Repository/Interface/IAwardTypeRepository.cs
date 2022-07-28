using A5.Models;
using A5.Data;

namespace A5.Data.Repository.Interface
{
    public interface IAwardTypeRepository
    {
        bool CreateAwardType(AwardType awardType);
        bool DisableAwardType(int awardTypeId, int userId);
        bool UpdateAwardType(AwardType awardType);
        AwardType? GetAwardTypeById(int id);
        IEnumerable<AwardType> GetAllAwardTypes();

    }
}