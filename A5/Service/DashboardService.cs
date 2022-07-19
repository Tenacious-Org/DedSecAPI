using System.ComponentModel.DataAnnotations;
using A5.Data.Repository;

namespace A5.Service
{
    public class DashboardService
    {
        private readonly AwardRepository _award;
        private readonly ILogger<DashboardService> _logger;
        public DashboardService(AwardRepository awardRepository,ILogger<DashboardService> logger)
        {
            _award=awardRepository;
            _logger=logger;
        }

        //filters all organisation,department and awardname
        public IEnumerable<object> GetAllAwards()
        {
            try
            {
                var winners = _award.GetAllAwardees();
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllAwards()  : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                 _logger.LogError("DashboardService: GetAllAwards(): (Error:{Message}",exception.Message);
                throw;
            }
        }

        
        public IEnumerable<object> GetDashboardDetails(int? organisationId, int? departmentId, int? awardId, DateTime? start, DateTime? end)
        {
            try
            {
                var winners = _award.GetDashboardDetails(organisationId, departmentId, awardId, start, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("AwardService: GetAllDateWise(DateTime start, DateTime end)() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("AwardService: GetAllDateWise(DateTime start, DateTime end)() : (Error:{Message}",exception.Message);
                throw;
            }
        }
    }
}