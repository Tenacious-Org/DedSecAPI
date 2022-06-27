using A5.Controller;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using A5.Data.Service;
using A5.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
namespace Testing.Controller
{
    public class AwardControllerTest
    {
        private readonly AwardController _awardController;
        private readonly Mock<ILogger<IAwardService>> _logger=new Mock<ILogger<IAwardService>>();
        private readonly Mock<IAwardService> _awardService=new Mock<IAwardService>();
        public AwardControllerTest()
        {
            _awardController=new AwardController(_logger.Object,_awardService.Object);

        }
         [Theory]
         [InlineData(0)]
        
        public void GetAwardById_ShouldReturnStatusCode400_WhereAwardIdIsNull(int Id)
        {         
             var Result=_awardController.GetAwardById(Id) as ObjectResult;
             Assert.Equal(400, Result?.StatusCode);
        }
        
        [Theory]
         [InlineData(1)]
        
        public void GetAwardById_ShouldReturnStatusCode200_WhereAwardIdIsValid(int Id)
        {         
             var Result=_awardController.GetAwardById(Id) as ObjectResult;
             Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void GetAwardById_shouldReturnStatusCode400_WhenAwardIdThrowsValidationException()
        {
            int Id=0;
            _awardService.Setup(obj=>obj.GetAwardById(Id)).Throws<ValidationException>();
            var Result=_awardController.GetAwardById(Id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void GetAwardById_ShouldReturnStatusCode400_WhenAwardIdThrowsException()
        {
            int Id=0;
            _awardService.Setup(obj=>obj.GetAwardById(Id)).Throws<Exception>();
            var Result=_awardController.GetAwardById(Id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
    }
}