using System;
using System.Collections.Generic;
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

        public DashboardController(DashboardService dash)
        {
            _dashboardService = dash;
        }

        [HttpGet("GetAllWinners")]
        public ActionResult GetPublisherDashboard()
        {
            try
            {
                var data = _dashboardService.GetAllWinners();
                return Ok(data);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet("GetAllOrgWise")]
        public ActionResult GetOrgWiseDashboard(int id)
        {
            try
            {
                var data = _dashboardService.GetAllByOrgwise(id);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
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
        [HttpGet("GetAllDateWise")]
        public ActionResult GetAllDateWiseDashboard(int orgid, int awdid, DateTime start, DateTime end)
        {
            try
            {
                var data = _dashboardService.GetAllDateWise(orgid, awdid, start, end);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
    }
}