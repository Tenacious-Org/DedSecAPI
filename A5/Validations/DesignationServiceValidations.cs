using A5.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using A5.Data.Service;
namespace A5.Validations
{
    public class DesignationServiceValidations
    {
        public static void ValidateGetByDepartment(int id)
        {
            if(id==0) throw new ValidationException("Organisation should not be null");

        }
    }
}