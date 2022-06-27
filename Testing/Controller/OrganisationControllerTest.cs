using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using A5.Controller;
using A5.Data.Service;
using A5.Data.Service.Interfaces;
using A5.Models;
using Testing.DB;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Testing.MockData;

namespace Testing.Controller
{
    public class OrganisationControllerTest
    {
        private readonly OrganisationController _organisationController;
        private readonly Mock<ILogger<OrganisationController>> _logger = new Mock<ILogger<OrganisationController>>();
        private readonly Mock<OrganisationService> _organisationService=new Mock<OrganisationService>();
        private readonly Mock<MockAppDBContext> _context=new Mock<MockAppDBContext>();
        public OrganisationControllerTest()
        {
            _organisationController=new OrganisationController(_logger.Object,MockAppDBContext.GetInMemoryDbContext(),_organisationService.Object);
        }
       
        [Fact]
        
        public void CreateOrganisation_ShouldReturnStatusCode400_WhereOrganisationObjectIsNull()
        {
            Organisation organisation=OrganisationMock.GetValidOrganisation();
            _organisationService.Setup(obj=>obj.Create(organisation)).Returns(true);


             var Result=_organisationController.Create(organisation) as ObjectResult;
             Assert.Equal(400, Result?.StatusCode);
        }
    }
}