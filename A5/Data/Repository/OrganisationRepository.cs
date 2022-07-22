using System.ComponentModel.DataAnnotations;
using A5.Models;
using A5.Data.Repository.Interface;
using A5.Data.Validations;

namespace A5.Data.Repository
{
    public class OrganisationRepository : EntityBaseRepository<Organisation>, IOrganisationRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EntityBaseRepository<Organisation>> _logger;
        private readonly OrganisationValidations _organisationvalidations;
        public OrganisationRepository(AppDbContext context, ILogger<EntityBaseRepository<Organisation>> logger,OrganisationValidations organisationvalidations) : base(context, logger)
        {
            _context = context;
            _logger = logger;
            _organisationvalidations=organisationvalidations;
        }
        
        //Creates organisation using organisation object
        public bool CreateOrganisation(Organisation organisation)
        {
            if (organisation == null) throw new ValidationException("Organisation should not be null");
            _organisationvalidations.CreateValidation(organisation);
            try
            {
                
                return Create(organisation);
            }

            catch (Exception exception)
            {
                _logger.LogError("OrganisationRepository: CreateOrganisation(Organisation organisation) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //Updates organisation using organisation object
        public bool UpdateOrganisation(Organisation organisation)
        {
           if (organisation == null) throw new ValidationException("Organisation should not be null");
            _organisationvalidations.UpdateValidation(organisation);
            try
            {
                return Update(organisation);
            }
            catch (Exception exception)
            {
                 _logger.LogError("OrganisationRepository: UpdateOrganisation(Organisation organisation) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //gets organisation by using organisation id
        public Organisation? GetOrganisationById(int organisationId)
        {
            if(organisationId<=0) throw new ValidationException("organisationId must be greater than zero");
            try
            {
                return GetById(organisationId);
            }
            catch (Exception exception)
            {
                _logger.LogError("OrganisationRepository:  GetByOrganisationId(int organisationId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //disables organisation using organisation id and current user id
        public bool DisableOrganisation(int organisationId, int userId)
        {
            if (organisationId <= 0) throw new ValidationException("Organisation Id must be greater than zero");
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _organisationvalidations.DisableValidation(userId);
            try
            {
                return Disable( organisationId, userId);

            }
            catch (Exception exception)
            {
                _logger.LogError("OrganisationRepository: DisableOrganisation(int  organisationId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //gets all organisatiob count by using organisation id
        public int GetCount(int organisationId)
        {
            if (organisationId <= 0) throw new ValidationException("Organisation Id must be greater than zero");
            var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive && nameof.OrganisationId == organisationId).Count();
            return checkEmployee;
        }
        
        //gets all organisation 
        public IEnumerable<Organisation> GetAllOrganisation()
        {

            try
            {
                return GetAll();
            }
            catch (Exception exception)
            {
               _logger.LogError("OrganisationRepository: GetAllOrganisation() : (Error:{Message}", exception.Message);
                throw;
            }
        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }
    }
}