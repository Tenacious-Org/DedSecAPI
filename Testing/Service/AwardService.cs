using A5.Controller;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using A5.Data;
using A5.Data.Repository;
using A5.Data.Service;
using Microsoft.AspNetCore.Mvc;
namespace Testing.Service
{
    public class AwardTestService
    {

        private readonly Mock<AppDbContext> _context=new Mock<AppDbContext>();
        private readonly Mock<MasterRepository> _master=new Mock<MasterRepository>();
        private readonly Mock<EmployeeService> _employee=new Mock<EmployeeService>();

        private readonly AwardService _awardService;
         public AwardTestService()
         {
            _awardService=new AwardService(_context.Object,_master.Object,_employee.Object);
         }
        public void GetAwardById_ShouldReturnTrue_whenIdIsValid(int Id)
        {
           Assert.Equal(true,_awardService.GetAwardById(Id));
        }
    }
}