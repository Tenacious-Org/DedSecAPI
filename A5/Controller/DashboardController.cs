using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data;
using A5.Data.Repository;
using A5.Data.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A5.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly AppDbContext _context;
        private readonly DashboardService _dash;

        public DashboardController(ILogger<DashboardController> logger, DashboardService dash, AppDbContext context)
        {
            _logger = logger;
            _dash = dash;
            _context = context;
        }

        [HttpGet("GetAllWinners")]
        public ActionResult GetPublisherDashboard()
        {
            try
            {
                var data = _dash.GetAllWinners();
                return Ok(data);
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }
        [HttpGet("GetAllOrgWise")]
        public ActionResult GetOrgWiseDashboard(int id)
        {
            try
            {
                var data = _dash.GetAllByOrgwise(id);
                return Ok(data);
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }   
    }
}