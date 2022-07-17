using System.ComponentModel.DataAnnotations;
using A5.Models;
using A5.Data.Repository.Interface;
using A5.Service.Validations;

namespace A5.Data.Repository
{
    public class OrganisationRepository : EntityBaseRepository<Organisation>, IOrganisationRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EntityBaseRepository<Organisation>> _logger;
        public OrganisationRepository(AppDbContext context, ILogger<EntityBaseRepository<Organisation>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public bool CreateOrganisation(Organisation organisation)
        {

            try
            {
                bool NameExists = _context.Organisations!.Any(nameof => nameof.OrganisationName == organisation.OrganisationName);
                if (NameExists) throw new ValidationException("Organisation Name already exists");
                return Create(organisation);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("OrganisationRepository: CreateOrganisation(Organisation organisation) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public bool UpdateOrganisation(Organisation organisation)
        {
            OrganisationServiceValidations.UpdateValidation(organisation);
            bool NameExists = _context.Organisations!.Any(nameof => nameof.OrganisationName == organisation.OrganisationName);
            if (NameExists) throw new ValidationException("Organisation Name already exists");
            try
            {
                return Update(organisation);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("OrganisationRepository: UpdateOrganisation(Organisation organisation) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public Organisation? GetOrganisationById(int id)
        {
            OrganisationServiceValidations.ValidateGetById(id);
            try
            {
                return GetById(id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("OrganisationRepository: GetByOrganisation(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public bool DisableOrganisation(int id, int employeeId)
        {
            OrganisationServiceValidations.DisableValidation(id);

            try
            {
                return Disable(id, employeeId);

            }
            catch (ValidationException exception)
            {

                _logger.LogError("OrganisationRepository: DisableOrganisation(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public int GetCount(int id)
        {
            var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive && nameof.OrganisationId == id).Count();
            return checkEmployee;
        }
        public IEnumerable<Organisation> GetAllOrganisation()
        {

            try
            {
                return GetAll();
            }
            catch (ValidationException exception)
            {
                _logger.LogError("OrganisationRepository: GetAllOrganisation() : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }
    }
}