using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using A5.Data;
using A5.Data.Repository;
using A5.Models;

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

        public IEnumerable<object> GetAllByOrgwise(int orgid)
        {
            try
            {
                if(orgid == 0)
                {
                    throw new ValidationException("Organisatioin ID should not be zero.");
                }
                var orgwise = _award.GetAllbyOrgwise(orgid);
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
        
        public IEnumerable<object> GetAllAwardwise(int awdid)
        {
            try
            {
                var deptwise = _award.GetAllAwardwise(awdid);
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
        public IEnumerable<object> GetAllAwardees()
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
        public IEnumerable<object> GetAllOrgandAward(int orgid, int awdid)
        {
            try
            {
                var winners = _award.GetAllOrgAndAwardwise(orgid, awdid);
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
        public IEnumerable<object> GetAllOrgandDep(int orgid, int depid)
        {
            try
            {
                var winners = _award.GetAllOrgAndDepwise(orgid, depid);
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

        public IEnumerable<object> GetAllOrgDepandAward(int orgid, int depid, int awdid)
        {
            try
            {
                var winners = _award.GetAllOrgDepandAwardwise(orgid, depid, awdid);
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
    }
}