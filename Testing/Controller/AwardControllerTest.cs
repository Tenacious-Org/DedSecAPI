using A5.Controller;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using A5.Data.Service;
using A5.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

    }
}