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
        public IEnumerable<object> GetAllByOrganisationwise(int organisationid)
        {
            try
            {
                if(organisationid == 0)
                {
                    throw new ValidationException("Organisatioin ID should not be zero.");
                }
                var orgwise = _award.GetAllbyOrgwise(organisationid);
                return orgwise.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,
                });
            }
             catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllByOrgWise(int orgid) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }


        //filters all organisation, department and awardname by award Id
        public IEnumerable<object> GetAllAwardwise(int awardid)
        {
            try
            {
                var deptwise = _award.GetAllAwardwise(awardid);
                return deptwise.Select(Award => new{
                    
                    organisation = Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                    
                    department = Award?.Awardee?.Designation?.Department?.DepartmentName,
                    
                    awardName = Award?.AwardType?.AwardName,
                });
            }
             catch(ValidationException exception)
            {
                _logger.LogError("DashboardService: GetAllByOrgWise(int orgid) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
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
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        //filters all organisation,department and awardname by using organisation id and award id
        public IEnumerable<object> GetAllOrganisationandAward(int organisationid, int awardid)
        {
            try
            {
                var winners = _award.GetAllOrgAndAwardwise(organisationid, awardid);
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
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        //get all by organisation id and department id
        public IEnumerable<object> GetAllOrganisationandDepartment(int organisationid, int departmentid)
        {
            try
            {
                var winners = _award.GetAllOrganisationandDepartment(organisationid, departmentid);
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
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        // filters all organisation,department and awardname by organisation id, department id and award id
        public IEnumerable<object> GetAllOrganisationDepartmentandAward(int organisationid, int departmentid, int awardid)
        {
            try
            {
                var winners = _award.GetAllOrgDepandAwardwise(organisationid, departmentid, awardid);
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
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }


        // filters all organisation,department,awardname and published date by organisation id, department id,award id, from date and to date
        public IEnumerable<object> GetAllFilteredDateWise(int orgid, int deptid, int awdid, DateTime start, DateTime end)
        {
            try
            {
                var winners = _award.GetAllFilteredDateWise(orgid, deptid, awdid, start, end);
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
        public IEnumerable<object> GetAllFilteredOrganisationandFromDate(int orgid, DateTime start)
        {
            try
            {
                var winners = _award.GetAllFilteredOrganisationandFromDateWise(orgid, start);
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
        public IEnumerable<object> GetAllFilteredOrgDepandFromDate(int orgid, int deptid, DateTime start)
        {
            try
            {
                var winners = _award.GetAllOrgDepandFromdatewise(orgid, deptid, start);
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
        //filters all organisation, departmnet, awardname and published date by using organisation id,award id and from date
        public IEnumerable<object> GetAllFilteredOrgAwdandFromDate(int orgid, int awdid, DateTime start)
        {
            try
            {
                var winners = _award.GetAllOrgDepandFromdatewise(orgid, awdid, start);
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

        //Filters all organisation, department, awardname and published date by using organisation Id, Department Id and To date
        public IEnumerable<object> GetAllFilteredOrgDepandToDate(int orgid, int deptid, DateTime end)
        {
            try
            {
                var winners = _award.GetAllOrgDepandFromdatewise(orgid, deptid, end);
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

        //Filters all organisation, department,awardname ad published date by using organisation Id, award Id Id and To date
        public IEnumerable<object> GetAllFilteredOrgAwdandToDate(int orgid, int awdid, DateTime end)
        {
            try
            {
                var winners = _award.GetAllOrgAwardandTodatewise(orgid, awdid, end);
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

        //Filters all organisation, department,awardname ad published date by using organisation Id and To date
        public IEnumerable<object> GetAllFilteredOrganisationandToDate(int orgid, DateTime end)
        {
            try
            {
                var winners = _award.GetAllFilteredOrganisationandToDateWise(orgid, end);
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
                _logger.LogError("Error: {Message}",exception.Message);
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
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        //Filters all organisation, department,awardname ad published date by using organisation Id, department Id, From date and To date
        public IEnumerable<object> GetAllOrgDeptDateWise(int orgid, int deptid, DateTime start, DateTime end)
        {
            try
            {
                var winners = _award.GetAllOrgDepDateWise(orgid, deptid, start, end);
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
        public IEnumerable<object> GetAllOrgAwdDateWise(int orgid, int awdid, DateTime start, DateTime end)
        {
            try
            {
                var winners = _award.GetAllOrgAwdDateWise(orgid, awdid, start, end);
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