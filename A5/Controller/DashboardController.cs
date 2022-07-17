using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using A5.Data;
using A5.Data.Repository;
using A5.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A5.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboard)
        {
            _dashboardService = dashboard;
        }


        //Get Method - To Retrieve All Awardees.

        [HttpGet("GetAllAwardees")]
        public ActionResult GetAllAwardeeDashboard()
        {
            try
            {
                var data = _dashboardService.GetAllAwardees();
                return Ok(data);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //Get Method - To retrieve all Awardees Based on their Organisation Wise List

        [HttpGet("GetAllOrgWise")]
        public ActionResult GetOrgWiseDashboard(int id)
        {
            try
            {
                var data = _dashboardService.GetAllByOrgwise(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                return Problem(exception.Message);
            }
        }


        //Get Method - To retrieve all Awardees Based on what Awards they Receiving List

        [HttpGet("GetAllAwardWise")]
        public ActionResult GetAwardWiseDashboard(int id)
        {
            try
            {
                var data = _dashboardService.GetAllAwardwise(id);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        //Get Method - To retrieve all Awardees Based on their Organisation Wise and what awards they receiving List.

        [HttpGet("GetAllOrgandAwardWise")]
        public ActionResult GetOrgandAwardWiseDashboard(int orgid, int awdid)
        {
            try
            {
                var data = _dashboardService.GetAllOrgandAward(orgid, awdid);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("GetAllOrgandDepWise")]
        public ActionResult GetOrgandDepWiseDashboard(int orgid, int depid)
        {
            try
            {
                var data = _dashboardService.GetAllOrgandDep(orgid, depid);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("GetAllOrgDepandAwardWise")]
        public ActionResult GetOrgandDepandAwardWiseDashboard(int orgid, int depid, int awdid)
        {
            try
            {
                var data = _dashboardService.GetAllOrgDepandAward(orgid, depid, awdid);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        
        [HttpGet("GetAllFilteredDateWise")]
        public ActionResult GetAllFilteredDateWiseDashboard(int orgid, int deptid, int awdid, DateTime start, DateTime end)
        {
            try
            {
                var data = _dashboardService.GetAllFilteredDateWise(orgid, deptid, awdid, start, end);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("GetAllFilteredFromDateWise")]
        public ActionResult GetAllFilteredFromDateWise(DateTime start)
        {
            try
            {
                var data = _dashboardService.GetAllFilteredFromDate(start);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("GetAllFilteredToDateWise")]
        public ActionResult GetAllFilteredToDateWise(DateTime end)
        {
            try
            {
                var data = _dashboardService.GetAllFilteredToDate(end);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("GetAllFilteredOrganisationandFromDateWise")]
        public ActionResult  GetAllFilteredOrganisationandFromDate(int orgid, DateTime start)
        {
            try
            {
                var data = _dashboardService. GetAllFilteredOrganisationandFromDate(orgid, start);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("GetAllFilteredOrganisationandToDateWise")]
        public ActionResult  GetAllFilteredOrganisationandToDate(int orgid, DateTime end)
        {
            try
            {
                var data = _dashboardService. GetAllFilteredOrganisayionandToDate(orgid, end);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
    }
}