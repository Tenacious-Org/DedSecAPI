using A5.Models;
using System.ComponentModel.DataAnnotations;

namespace A5.Data.Service.Validations
{
    public class StatusServiceValidations
    {
        public static void ValdiateGetById(int id)
        {
            if(id==0) throw new ValidationException("Status Id should not be null");
        }
    }
}