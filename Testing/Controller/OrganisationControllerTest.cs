using A5.Controller;
using A5.Data.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Testing.DB;
using Microsoft.AspNetCore.Mvc;
using A5.Models;
using Testing.MockData;
using System.ComponentModel.DataAnnotations;
using System;
namespace Testing.Controller
{
    public class OrganisationControllerTest
    {
        private readonly OrganisationController _organisationController;
        private readonly Mock<ILogger<IOrganisationService>> _logger=new Mock<ILogger<IOrganisationService>>();
        private readonly Mock<IOrganisationService> _organisationService=new Mock<IOrganisationService>();
        public OrganisationControllerTest()
        {
            _organisationController=new OrganisationController(_logger.Object,_organisationService.Object);
        }
        [Theory]
        [InlineData(null)]
        public void CreateOrganisation_ShouldReturnStatusCode400_whenOrganisationIsNUll(Organisation organisation)
        {
            var Result=_organisationController.Create(organisation) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void CreateOrganisation_ShouldReturnStatusCode200_WhenOrganisationIsValid()
        {
            Organisation organisation=OrganisationMock.GetValidOrganisation();
            _organisationService.Setup(obj=>obj.CreateOrganisation(organisation)).Returns(true);
            var Result=_organisationController.Create(organisation) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void CreateOrganisation_ShouldReturnStatusCode400_WhenCreateThrowsValidationException()
        {
            Organisation organisation=new Organisation();
            _organisationService.Setup(obj=>obj.CreateOrganisation(organisation)).Throws<ValidationException>();
            var Result=_organisationController.Create(organisation) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void CreateOrganisation_ShouldReturnStatusCode400_WhenCreateThrowsException()
        {
            Organisation organisation=new Organisation();
            _organisationService.Setup(obj=>obj.CreateOrganisation(organisation)).Throws<Exception>();
            var Result=_organisationController.Create(organisation) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Theory]
        [InlineData(null)]
        public void UpdateOrganisation_ShouldReturnStatusCode400_WhenUpdateOrganisationIsNull(Organisation organisation)
        {
            var Result=_organisationController.Update(organisation) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);


        }
        // [Fact]
        // public void UpdateOrganisation_Should
    }
}