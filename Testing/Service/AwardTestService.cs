using A5.Controller;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using A5.Data;
using A5.Data.Repository;
using A5.Data.Service;
using A5.Models;
using Testing.MockData;
using Microsoft.AspNetCore.Mvc;
using Testing.DB;
using System;
using A5.Data.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
namespace Testing.Service
{
    public class AwardTestService
    {

        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        private readonly AwardService _awardService;
         public AwardTestService()
         {
            _context=MockAppDBContext.GetInMemoryDbContext();
            _awardService=new AwardService(_context,_master);
         }
        //  [Theory]
        //  [InlineData(0)]
        // public void GetAwardById_ShouldThrowsException_whenIdIsNull(int Id)
        // {
        //     Award award=AwardMock.GetValidAward();
        //     _awardService.Setup(obj=>obj.GetAwardById(Id)).Throws<ValidationException>();
            
        // }
       
    }
}