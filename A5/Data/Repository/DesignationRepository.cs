using System.ComponentModel.DataAnnotations;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Data.Validations;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class DesignationRepository : EntityBaseRepository<Designation>, IDesignationRepository
    {
        private readonly AppDbContext _context;
         private readonly ILogger<EntityBaseRepository<Designation>> _logger;
         private readonly DesignationValidations _designationValidations;
        public DesignationRepository(AppDbContext context,ILogger<EntityBaseRepository<Designation>> logger,DesignationValidations designationValidations) : base(context,logger)
        {
            _context = context;
            _logger=logger;
            _designationValidations=designationValidations;
        }

    
        //to get designations by departmentId
         public IEnumerable<Designation> GetDesignationsByDepartmentId(int departmentId)
         {
            if(departmentId==0) throw new ValidationException("Department Id should not be zero");
            try
            {
                var data =  _context.Set<Designation>().Where(nameof =>nameof.DepartmentId == departmentId && nameof.IsActive == true).ToList();
                var count = data.Count;
                if(count != 0)
                {
                    return data;
                }
                else
                {
                    throw new ValidationException(" Department not Found!! ");
                }
            }
            catch(Exception exception)
            {
                _logger.LogError("DesignationRespository : GetDesignationsByDepartmentId(departmentId : {departmentId}) : (Error:{Message}",departmentId,exception.Message);
               throw;
            }
         }

         //to get all designations
          public IEnumerable<Designation> GetAllDesignation()
        {
            try
            {
                var designations = _context.Set<Designation>().Where(nameof =>nameof.IsActive == true).Include("Department").ToList();
                return designations;
            }
            catch(Exception exception)
            {
                 _logger.LogError("DesignationRespository: GetAllDesignation() : (Error:{Message}",exception.Message);
                throw;
            }
            
        }

        //to get designation by using designation id
        public Designation? GetDesignationById(int designationId)
        {
            if(designationId<=0) throw new ValidationException(" Designation Id must be greatet than zero");
            try{
                return GetById(designationId);
            }
            catch(Exception exception)
            {
               _logger.LogError("DesignationRespository: GetDesginationById(int designationId) : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //creates designation using designation object
          public bool CreateDesignation(Designation designation)
        {
            _designationValidations.CreateValidation(designation);
            try{
                return Create(designation);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("DesignationRespository: CreateDesignation(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
               _logger.LogError("DesignationRespository: CreateDesignation(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
        }

        // to get designation count by using designation id
        public int GetCount(int designationId)
        {
             var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.DesignationId == designationId).Count();
             return checkEmployee;
        }
         public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }

        // to update designation using designation object
        public bool UpdateDesignation(Designation designation)
        {
            _designationValidations.UpdateValidation(designation);
            
            try{
                return Update(designation);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DesignationRespository: UpdateDesignation(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
              _logger.LogError("DesignationRespository: UpdateDesignation(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //to disable designation using designation id and current user id
        public bool DisableDesignation(int designationId,int employeeId)
        {
           
            try{
                return Disable(designationId,employeeId);
            }
            catch(Exception exception)
            {
                _logger.LogError("DesignationRespository: DisableDesignation(int designationId,int employeeId) : (Error:{Message}",exception.Message);
                throw;
            }
        }
    }
}