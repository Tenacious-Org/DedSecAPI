using A5.Controller;
using A5.Service;
using A5.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using A5.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UnitTesting.MockData;

namespace UnitTesting.Controller
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
           var organisation=OrganisationMock.ValidOrganisation();
            _organisationService.Setup(obj=>obj.CreateOrganisation(organisation)).Returns(true);
            var Result=_organisationController.Create(organisation) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
         [Fact]
        public void CreateOrganisation_ShouldReturnStatusCode400_WhenCreateThrowsValidationException()
        {
             var organisation=OrganisationMock.ValidOrganisation();
            _organisationService.Setup(obj=>obj.CreateOrganisation(organisation)).Throws<ValidationException>();
            var Result=_organisationController.Create(organisation) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void CreateOrganisation_ShouldReturnStatusCode500_WhenCreateThrowsException()
        {
            var organisation=OrganisationMock.ValidOrganisation();
            _organisationService.Setup(obj=>obj.CreateOrganisation(organisation)).Throws<Exception>();
            var Result=_organisationController.Create(organisation) as ObjectResult;
            Assert.Equal(500,Result?.StatusCode);
        }
        [Fact]
        public void GetOrganisationById_ShouldReturnStatusCode400_whenOrganisationIdIsNull()
        {
            int id=0;
            var Result=_organisationController.GetOrganisationById(id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void GetOrganisationById_ShouldReturnStatusCode200_WhenOrganisationIdIsValid()
        {
             var organisation=OrganisationMock.ValidOrganisation();             
            _organisationService.Setup(obj=>obj.GetOrganisationById(1)).Returns(organisation);
        var Result=_organisationController.GetOrganisationById(1) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void GetOrganisationById_ShouldReturnStatusCode400_WhenGetByOrganisationIdThrowsValidationException()
        {
            _organisationService.Setup(obj=>obj.GetOrganisationById(1)).Throws<ValidationException>();
            var Result=_organisationController.GetOrganisationById(1) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void GetOrganisationById_ShouldReturnStatusCode500_WhenGetByOrganisationIdThrowsException()
        {
            _organisationService.Setup(obj=>obj.GetOrganisationById(1)).Throws<Exception>();
            var Result=_organisationController.GetOrganisationById(1) as ObjectResult;
            Assert.Equal(500,Result?.StatusCode);
        }
        [Theory]
        [InlineData(null)]
        public void UpdateOrganisation_ShouldReturnStatusCode400_WhenOrganisationIsNull(Organisation organisation)
        {
           var Result=_organisationController.Update(organisation) as ObjectResult;
           Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void UpdateOrganisation_ShouldReturnStatusCode200_WhenUpdateIsValid()
        {
            var organisation=OrganisationMock.ValidOrganisation();
            _organisationService.Setup(obj=>obj.UpdateOrganisation(organisation)).Returns(true);
            var Result=_organisationController.Update(organisation) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void UpdateOrganisation_ShouldReturnStatusCode400_whenUpdateThrowsValidationException()
        {
            var organisation=OrganisationMock.ValidOrganisation();
            _organisationService.Setup(obj=>obj.UpdateOrganisation(organisation)).Throws<ValidationException>();
            var Result=_organisationController.Update(organisation) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void UpdateOrganisation_ShouldReturnStatusCode500_WhenUpdateThrowsException()
        {
             var organisation=OrganisationMock.ValidOrganisation();
             _organisationService.Setup(obj=>obj.UpdateOrganisation(organisation)).Throws<Exception>();
             var Result=_organisationController.Update(organisation) as ObjectResult;
             Assert.Equal(500,Result?.StatusCode);
        }
        [Fact]
        public void GetAllOrganisation_ShouldReturnStatusCode200_WhenGetAllOrganisaationIsValid()
        {
            var organisation=OrganisationMock.GetListOfOrganisations();
            _organisationService.Setup(obj=>obj.GetAllOrganisation()).Returns(organisation);
            var Result=_organisationController.GetAllOrganisation() as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);

        }
        [Fact]
        public void GetAllOrganisation_ShouldReturnStatusCode500_WhenGetAllOrganisationThrowsException()
        {
            var organisation=OrganisationMock.GetListOfOrganisations();
            _organisationService.Setup(obj=>obj.GetAllOrganisation()).Throws<Exception>();
            var Result=_organisationController.GetAllOrganisation() as ObjectResult;
            Assert.Equal(500,Result?.StatusCode);
        }
        [Theory]
        [InlineData(0)]
        public void DisableOrganisation_ShouldReturnStatusCode400_WhenOrganisationIdIsNull(int organisationId)
        {
            var Result=_organisationController.Disable(organisationId) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void DisableOrganisation_ShouldReturnStatusCode200_WhenDisableOrganisationIsValid()
        {
            var organisation=OrganisationMock.MockDisableOrganisation();
            _organisationService.Setup(obj=>obj.DisableOrganisation(1,0)).Returns(true);
            var Result=_organisationController.Disable(1) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void DisableOrganisation_ShouldReturnStatusCode400_WhenDisableOrganisationThrowsValidationException()
        {
            _organisationService.Setup(obj=>obj.DisableOrganisation(1,0)).Throws<ValidationException>();
            var Result=_organisationController.Disable(1) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void DisableOrganisation_ShouldReturnStatusCode500_WhenDisableOrganisationThrowsException()
        {
            _organisationService.Setup(obj=>obj.DisableOrganisation(1,0)).Throws<Exception>();
            var Result=_organisationController.Disable(1) as ObjectResult;
            Assert.Equal(500,Result?.StatusCode);
        }
        [Fact]
        public void DisableOrganisation_ShouldReturnStatusCode200_WhenEmployeeCountIsGreaterThanZero()
        {
            int count=10;
            _organisationService.Setup(obj=>obj.GetCount(1)).Returns(count);
            var Result=_organisationController.Disable(1) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);

        }

    
    }
}