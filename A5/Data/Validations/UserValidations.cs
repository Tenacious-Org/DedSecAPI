using A5.Data;
using A5.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace A5.Data.Validations
{
    public  class UserValidations
    {
        private readonly AppDbContext _context;
        public UserValidations(AppDbContext context)
        {
            _context=context;
        }
        public bool AdminValidation(int? userId){
            var Admin=_context.Set<Employee>().Include("Designation").FirstOrDefault(nameof=>nameof.Id==userId);
            if(Admin?.Designation!.RoleId!=5)  throw new ValidationException("This user doesn't have access");
            else return true;
        }
    }
}