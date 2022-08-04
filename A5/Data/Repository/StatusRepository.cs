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

        
        //gets status by using status id
        public Status? GetStatusById(int statusId)
        {
            if(statusId<=0) throw new ValidationException("Id should not be zero or negative");
            try
            {
                var status = _context.Set<Status>().FirstOrDefault(nameof => nameof.Id == statusId);
                return status;
            }
            catch (Exception exception)
            {
                _logger.LogError("StatusRepository : GetStatusById(id : {id}) : (Error: {Message}", statusId,exception.Message);
                throw;
            }
        }
        
        //gets all status
        public IEnumerable<Status> GetAllStatus()
        {

            try
            {
                var status = _context.Set<Status>().ToList();
                return status;
            }
            catch (Exception exception)
            {
                _logger.LogError("StatusRepository : GetAllStatus() : (Error: {Message}",exception.Message);
                throw;
            }
        }

         public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }
    }
}