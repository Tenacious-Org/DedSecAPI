using System.ComponentModel.DataAnnotations;
using A5.Models;
using A5.Data.Repository.Interface;

namespace A5.Data.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<IStatusRepository> _logger;
        public StatusRepository(AppDbContext context, ILogger<IStatusRepository> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public Status? GetStatusById(int id)
        {
            try
            {
                var status = _context.Set<Status>().FirstOrDefault(nameof => nameof.Id == id);
                return status;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public IEnumerable<Status> GetAllStatus()
        {

            try
            {
                var status = _context.Set<Status>().ToList();
                return status;
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