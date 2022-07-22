using A5.Models;
using A5.Data;

namespace A5.Data.Repository.Interface
{
    public interface IAwardTypeRepository
        {
        public bool CreateAwardType(AwardType awardType);
        public bool DisableAwardType(int awardTypeId,int userId);
        public bool UpdateAwardType(AwardType awardType);
        public AwardType? GetAwardTypeById(int id);
        public IEnumerable<AwardType> GetAllAwardTypes();

    }
}