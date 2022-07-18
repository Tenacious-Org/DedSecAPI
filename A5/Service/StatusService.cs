using A5.Models;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using A5.Service.Validations;
using A5.Data.Repository.Interface;

namespace A5.Service
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ILogger<StatusService> _logger;

        public StatusService(ILogger<StatusService> logger, IStatusRepository statusRepository)
        {
            _logger = logger;
            _statusRepository = statusRepository;
        }


        public Status? GetStatusById(int id)
        {
            if (id <= 0) throw new ValidationException("organisationId ");
            try
            {
                return _statusRepository.GetStatusById(id);
            }
             catch (ValidationException exception)
            {
                _logger.LogError("StatusService: GetStatusById(id : {id}) : (Error:{Message}", id,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("StatusService: GetStatusById(id : {id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
        }
        public IEnumerable<Status> GetAllStatus()
        {

            try
            {
                return _statusRepository.GetAllStatus();
            }
            catch (Exception exception)
            {
                _logger.LogError("StatusService: GetAllStatus() : (Error:{Message}", exception.Message);
                throw;
            }
        }
    }
}



