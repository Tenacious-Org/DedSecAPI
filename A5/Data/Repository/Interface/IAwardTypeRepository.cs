using A5.Models;
using A5.Data;

namespace A5.Data.Repository.Interface
{
    public interface IAwardTypeRepository
        {
        public bool CreateAwardType(AwardType awardType,int employeeId);
        public bool DisableAwardType(int id,int employeeId);
        public bool UpdateAwardType(AwardType awardType,int employeeId);
        public AwardType? GetAwardTypeById(int id);
        public IEnumerable<AwardType> GetAllAwardTypes();

    }
}