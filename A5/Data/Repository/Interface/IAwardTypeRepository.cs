using A5.Models;
using A5.Data;

namespace A5.Data.Repository.Interface
{
    public interface IAwardTypeRepository
        {
        public bool CreateAwardType(AwardType entity);
        public bool DisableAwardType(int id);
        public bool UpdateAwardType(AwardType entity);
        public AwardType? GetAwardTypeById(int id);
        public IEnumerable<AwardType> GetAllAwardTypes();

    }
}