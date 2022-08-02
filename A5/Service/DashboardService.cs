using System.ComponentModel.DataAnnotations;
using A5.Data.Repository;
using A5.Data.Repository.Interface;
using A5.Service.Interfaces;

namespace A5.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IAwardRepository _award;
        private readonly ILogger<IDashboardService> _logger;
        public DashboardService(IAwardRepository awardRepository,ILogger<IDashboardService> logger)
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

        //filters all organisation,department,award,from date and to date
        public IEnumerable<object> GetDashboardDetailsByFilters(int organisationId, int departmentId, int awardId, DateTime start, DateTime end)
        {
            try
            {
                var winners = _award.GetDashboardDetailsByFilters(organisationId, departmentId, awardId, start, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    awardeeName = Award?.Awardee?.FirstName,

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