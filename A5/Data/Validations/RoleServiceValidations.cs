using A5.Models;
using System.ComponentModel.DataAnnotations;
namespace A5.Service.Validations
{
    public static class RoleServiceValidations
    {
        public static bool ValidateGetById(int id)
        {
            if(id==0) throw new ValidationException("Role Id should not be null");
            if(id<0) throw new ValidationException("Role Id should not be negative");
            else return true;
        }
    }
}