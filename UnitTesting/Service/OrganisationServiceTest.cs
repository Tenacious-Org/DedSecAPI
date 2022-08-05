using A5.Data.Repository;
using A5.Data.Validations;
using A5.Service;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTesting.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using A5.Data.Repository.Interface;

namespace UnitTesting.Service
{
    public class OrganisationServiceTest
    {
        private readonly OrganisationService _organisationService;
        private readonly Mock<ILogger<OrganisationService>> _logger=new Mock<ILogger<OrganisationService>>();
        private readonly Mock<IOrganisationRepository> _organisationRepository=new Mock<IOrganisationRepository>();
        private readonly Mock<IOrganisationValidations> _organisationValidations=new Mock<IOrganisationValidations>();
      
        public OrganisationServiceTest()
        {
           _organisationService=new  OrganisationService(_logger.Object,_organisationRepository.Object, _organisationValidations.Object);
        }
        [Fact]
        public void CreateOrganisation_ShouldReturnTrue_WhenOrganisationIsValid()
        {
            var organisation=OrganisationMock.ValidOrganisation();
            _organisationRepository.Setup(obj=>obj.CreateOrganisation(organisation)).Returns(true);
            var Result=_organisationService.CreateOrganisation(organisation);
             Result.Should().BeTrue();
        }
        
    }
}