using System.Collections.Generic;
using System.Linq;
using A5.Models;
namespace A5.Service.Interfaces
{
    public interface IAwardTypeService
    {
        public bool CreateAwardType(AwardType awardType);
        public bool DisableAwardType(int id,int employeeId);
        public bool UpdateAwardType(AwardType awardType);
        public AwardType? GetAwardTypeById(int awardTypeId);
        public IEnumerable<AwardType> GetAllAwardType();
        public object ErrorMessage(string ValidationMessage);


    }
}