using A5.Controller;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using A5.Data.Service;
using A5.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using A5.Models;
using Testing.MockData;
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
        
        public void GetAwardById_ShouldReturnStatusCode400_WhenAwardIdIsNull(int Id)
        {         
             var Result=_awardController.GetAwardById(Id) as ObjectResult;
             Assert.Equal(400, Result?.StatusCode);
        }
        
        [Theory]
         [InlineData(1)]
        
        public void GetAwardById_ShouldReturnStatusCode200_WhenAwardIdIsValid(int Id)
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
        [Fact]
        public void RaiseRequest_ShouldReturnStatusCode400_WhenIdIsNull()
        {
            Award award=new Award();
            int id=0;
            var Result=_awardController.RaiseRequest(award,0) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void RaiseRequest_ShouldReturnStatusCode200_WhenIdIsValid()
        {
           Award award=new Award();
            int id=1;
            _awardService.Setup(obj=>obj.RaiseRequest(award,id)).Returns(true) ;
            var Result=_awardController.RaiseRequest(award,id) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);

        }
        [Fact]
        public void RaiseRequest_ShouldReturnStatusCode400_WhenIdThrowsValidationException()
        {
            Award award=new Award();
            int id=1;
            _awardService.Setup(obj=>obj.RaiseRequest(award,id)).Throws<ValidationException>();
            var Result=_awardController.RaiseRequest(award,id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void RaiseRequest_ShouldReturnStatusCode400_WhenIdThrowsException()
        {
            Award award=new Award();
            int id=1;
            _awardService.Setup(obj=>obj.RaiseRequest(award,id)).Throws<Exception>();
            var Result=_awardController.RaiseRequest(award,id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void Approval_ShouldReturnStatusCode400_WhenIdIsNull()
        {
            Award award=new Award();
            int id=0;
            var Result=_awardController.Approval(award,0) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void Approval_ShouldReturnStatusCode200_WhenIdIsValid()
        {
            Award award=new Award();    
            int id=1;
            _awardService.Setup(obj=>obj.Approval(award,id)).Returns(true);
            var Result=_awardController.Approval(award,id) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void Approval_ShouldReturnStatusCode400_WhenIdThrowsValidationException()
        {
            Award award=new Award();
            int id=1;
           _awardService.Setup(obj=>obj.Approval(award,id)).Throws<ValidationException>();
            var Result=_awardController.Approval(award,id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void Approval_ShouldReturnStatusCode400_WhenIdThrowsException()
        {
            Award award=new Award();
            int id=1;
              _awardService.Setup(obj=>obj.Approval(award,id)).Throws<Exception>();
            var Result=_awardController.Approval(award,id) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Theory]
        [InlineData(null)]
        public void AddComment_ShouldReturnStatusCode400_WhenCommentIsNull(Comment comment)
        {
            var Result=_awardController.AddComment(comment) as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
        public void AddComment_ShouldReturnStatusCode200_WhenCommentIsValid()
        {
           Comment comment=new Comment();
           _awardService.Setup(obj=>obj.AddComment(comment)).Returns(true);
            var Result=_awardController.AddComment(comment) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        // public void AddComment_ShouldReturnStatusCode400_WhenCommentThrowsValidationException()
        // {
        //     Comment comment=new Comment();
        //     _
        // }
    }
}