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
        [Fact]
        public void UpdateOrganisation_ShouldReturnStatusCode200_WhenUpdateOrganisationIsValid()
        {
            Organisation organisation=OrganisationMock.GetValidOrganisation();
            _organisationService.Setup(obj=>obj.UpdateOrganisation(organisation)).Returns(true);
            var Result=_organisationController.Update(organisation) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);

        }
         [Fact]
        public void UpdateOrganisation_ShouldReturnStatusCode400_WhenUpdateThrowsValidationException()
        {
            Organisation organisation=new Organisation();
            _organisationService.Setup(obj=>obj.UpdateOrganisation(organisation)).Throws<ValidationException>();
            var Result=_organisationController.Update(organisation) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
         [Fact]
        public void UpdateOrganisation_ShouldReturnStatusCode400_WhenUpdateThrowsException()
        {
            Organisation organisation=new Organisation();
            _organisationService.Setup(obj=>obj.UpdateOrganisation(organisation)).Throws<Exception>();
            var Result=_organisationController.Update(organisation) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void GetAllOrganisation_ShouldReturnStatusCode200_WhenGetAllIsValid()
        {
            var organisation=OrganisationMock.GetListOfOrganisations();
            _organisationService.Setup(obj=>obj.GetAll()).Returns(organisation);
            var Result=_organisationController.GetAllOrganisation() as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void GetAllOrganisation_shouldReturnStatusCode400_WhenGetAllThrowsValidationException()
        {
            var organisation=OrganisationMock.GetListOfOrganisations();
            _organisationService.Setup(obj=>obj.GetAll()).Throws<ValidationException>();
            var Result=_organisationController.GetAllOrganisation() as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
         [Fact]
        public void GetAllOrganisation_shouldReturnStatusCode400_WhenGetAllThrowsException()
        {
            var organisation=OrganisationMock.GetListOfOrganisations();
            _organisationService.Setup(obj=>obj.GetAll()).Throws<Exception>();
            var Result=_organisationController.GetAllOrganisation() as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Theory]
        [InlineData(0)]
        public void GetByOrganisationId_ShouldReturnStatusCode400_WhenGetByOrganisationIdIsNull(int id)
        {
            var Result=_organisationController.GetByOrganisationId(0) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void GetByOrganisationId_shouldReturnStatusCode200_whenGetByOrganisationIdIsValid()
        {
            int id=1;
            var organisation=OrganisationMock.GetValidOrganisation();
            _organisationService.Setup(obj=>obj.GetByOrganisation(id)).Returns(organisation);
            var Result=_organisationController.GetByOrganisationId(id) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
         public void GetByOrganisationId_shouldReturnStatusCode200_whenGetByOrganisationIdThrowsValidationException()
        {
            int id=1;
            var organisation=OrganisationMock.GetValidOrganisation();
            _organisationService.Setup(obj=>obj.GetByOrganisation(id)).Throws<ValidationException>();
            var Result=_organisationController.GetByOrganisationId(id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
         public void GetByOrganisationId_shouldReturnStatusCode200_whenGetByOrganisationIdThrowsException()
        {
            int id=1;
            var organisation=OrganisationMock.GetValidOrganisation();
            _organisationService.Setup(obj=>obj.GetByOrganisation(id)).Throws<Exception>();
            var Result=_organisationController.GetByOrganisationId(id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }

    }
}