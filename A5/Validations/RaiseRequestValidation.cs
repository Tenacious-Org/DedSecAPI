using A5.Data.Service;
using A5.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Validations
{
    public class RaiseRequestValidation
    {
        public bool RequestValidation(Award award,int id)
        {
            if(award.AwardeeId==null) throw new ValidationException("Awardee should not be null");
            if(award.AwardTypeId==null) throw new ValidationException("Award Type Should not be null");
            if(award.Reason==null) throw new ValidationException("Reason for award should not be null");
            else return true;
        }
    }
}