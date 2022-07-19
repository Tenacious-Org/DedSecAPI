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

        //filters all organisation,department and awardname by organisation Id
        public IEnumerable<object> GetAllByOrganisationwise(int organisationId)
        {
            try
            {
                if(organisationId == 0)
                {
                    throw new ValidationException("Organisatioin Id should not be zero.");
                }
                var organisationWise = _award.GetAllByOrganisationWise(organisationId);
                return organisationWise.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,
                });
            }
             catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllByOrganisationWise(int organisationId): (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                 _logger.LogError("DashboardService: GetAllByOrganisationWise(int organisationId): (Error:{Message}",exception.Message);
                throw;
            }
        }


        //filters all organisation, department and awardname by award Id
        public IEnumerable<object> GetAllAwardWise(int awardId)
        {
            try
            {
                var departmentWise = _award.GetAllAwardWise(awardId);
                return departmentWise.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,
                });
            }
             catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllAwardWise(int awardId) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                 _logger.LogError("DashboardService: GetAllAwardWise(int awardId) : (Error:{Message}",exception.Message);
                throw;
            }
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
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                 _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //filters all organisation,department and awardname by using organisation id and award id
        public IEnumerable<object> GetAllOrganisationAndAward(int organisationId, int awardId)
        {
            try
            {
                var winners = _award.GetAllOrganisationAndAward(organisationId, awardId);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
               
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //get all by organisation id and department id
        public IEnumerable<object> GetAllOrganisationAndDepartment(int organisationId, int departmentId)
        {
            try
            {
                var winners = _award.GetAllOrganisationAndDepartment(organisationId, departmentId);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
               _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        // filters all organisation,department and awardname by organisation id, department id and award id
        public IEnumerable<object> GetAllOrganisationDepartmentAndAwardwise(int organisationId, int departmentId, int awardId)
        {
            try
            {
                var winners = _award.GetAllOrganisationDepartmentAndAwardwise(organisationId, departmentId, awardId);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }


        // filters all organisation,department,awardname and published date by organisation id, department id,award id, from date and to date
        public IEnumerable<object> GetAllFilteredDateWise(int organisationId, int departemrntId, int awdid, DateTime start, DateTime end)
        {
            try
            {
                var winners = _award.GetAllFilteredDateWise(organisationId, departemrntId, awdid, start, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }


        //filters all organisation,department,awardname and published date by organisation id and from date 
        public IEnumerable<object> GetAllFilteredOrganisationandFromDate(int organisationId, DateTime start)
        {
            try
            {
                var winners = _award.GetAllFilteredOrganisationandFromDateWise(organisationId, start);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        //filters all organisation,department,awardname and published date by using organisation id,department id and from date
        public IEnumerable<object> GetAllFilteredOrganisationDepartmentAndFromDate(int organisationId, int departmentId, DateTime start)
        {
            try
            {
                var winners = _award.GetAllOrganisationDepartmentAndFromDateWise(organisationId, departmentId, start);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }
        //filters all organisation, departmnet, awardname and published date by using organisation id,award id and from date
        public IEnumerable<object> GetAllOrganisationDepartmentAndFromDateWise(int organisationId, int awardId, DateTime start)
        {
            try
            {
                var winners = _award.GetAllOrganisationDepartmentAndFromDateWise(organisationId, awardId, start);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
               _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //Filters all organisation, department, awardname and published date by using organisation Id, Department Id and To date
        public IEnumerable<object> GetAllFilteredOrganisationDepartmentAndToDate(int organisationId, int departmentId, DateTime end)
        {
            try
            {
                var winners = _award.GetAllOrganisationDepartmentAndToDateWise(organisationId, departmentId, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //Filters all organisation, department,awardname ad published date by using organisation Id, award Id Id and To date
        public IEnumerable<object> GetAllFilteredOrganisationAwarddAndToDate(int organisationId, int awardId, DateTime end)
        {
            try
            {
                var winners = _award.GetAllOrganisationAwardAndToDateWise(organisationId, awardId, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                
                 _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //Filters all organisation, department,awardname ad published date by using organisation Id and To date
        public IEnumerable<object> GetAllFilteredOrganisationAndToDate(int organisationId, DateTime end)
        {
            try
            {
                var winners = _award.GetAllFilteredOrganisationAndToDateWise (organisationId, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                 _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

       //Filters all organisation, department,awardname ad published date by using To date
        public IEnumerable<object> GetAllFilteredFromDate(DateTime start)
        {
            try
            {
                var winners = _award.GetAllFilteredFromDateWise(start);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //Filters all organisation, department,awardname ad published date by using To date
        public IEnumerable<object> GetAllFilteredToDate(DateTime end)
        {
            try
            {
                var winners = _award.GetAllFilteredToDateWise(end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
               
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        //Filters all organisation, department,awardname ad published date by using organisation Id, department Id, From date and To date
        public IEnumerable<object> GetAllOrgDeptDateWise(int organisationId, int departmentId, DateTime start, DateTime end)
        {
            try
            {
                var winners = _award.GetAllOrganisationDepartmentDateWise (organisationId, departmentId, start, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
        }

        public IEnumerable<object> GetAllDateWise(DateTime start, DateTime end)
        {
            try
            {
                var winners = _award.GetAllDateWise(start, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        //Filters all organisation, department,awardname ad published date by using organisation Id, award Id, From date and To date
        public IEnumerable<object> GetAllOrganisationAwardAndDateWise(int organisationId, int awardId, DateTime start, DateTime end)
        {
            try
            {
                var winners = _award.GetAllOrganisationAwardDateWise(organisationId, awardId, start, end);
                return winners.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,

                    publishedDate = Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
    }
}