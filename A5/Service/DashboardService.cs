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
        public IEnumerable<object> GetAllByDeptwise(int orgid)
        {
            try
            {
                var orgwise = _award.GetAllbyDeptwise(orgid);
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
        public IEnumerable<object> GetAllAwardwise(int orgid)
        {
            try
            {
                var orgwise = _award.GetAllAwardwise(orgid);
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
        public IEnumerable<object> GetAllWinners()
        {
            try
            {
                var winners = _award.GetAllWinners();
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
    }
}