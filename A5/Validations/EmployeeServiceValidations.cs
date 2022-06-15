using A5.Models;
using System.ComponentModel.DataAnnotations;
namespace A5.Validations
{
    public class EmployeeServiceValidations
    {
        public static void ValidateGetByHr(int id)
        {
            if(id==0) throw new ValidationException("HR Id should not be null");
        }
        public static void ValidateGetByReportingPerson(int id)
        {
            if(id==0) throw new ValidationException("Reporting Person Id should not be null");
        }
        public static void ValidateGetByDepartment(int id)
        {
            if(id==0) throw new ValidationException("Department Id should not be null");
        }
       public static void ValidateGetByRequester(int id)
       {
            if(id==0) throw new ValidationException("Requester ID should not be null");
       }
       public static void ValidateGetByOrganisation(int id)
       {
           if(id==0) throw new ValidationException("Organisation id should not be null");
       }
       public static void GetEmployeeById(int id)
       {
        if(id==0) throw new ValidationException("Employee id should not be null");
       }
    }
}