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
    public class DepartmentControllerTest
    {
        private readonly DepartmentController _departmentController;
        private readonly Mock<ILogger<IDepartmentService>> _logger=new Mock<ILogger<IDepartmentService>>();
        private readonly Mock<IDepartmentService> _departmentService=new Mock<IDepartmentService>();
        public DepartmentControllerTest()
        {
            _departmentController=new DepartmentController(_logger.Object,_departmentService.Object);
        }

        //  [Theory]
        // [InlineData(null)]
        // public void CreateDepartment_ShouldReturnStatusCode400_whenDepartmentIsNUll(Department department)
        // {
        //     var Result=_departmentController.Create(department) as ObjectResult;
        //     Assert.Equal(400,Result?.StatusCode);
        // }
        [Fact]
        public void CreateDepartment_ShouldReturnStatusCode200_WhenDepartmentIsValid()
        {
            Department department=DepartmentMock.GetvalidDepartment();
            _departmentService.Setup(obj=>obj.CreateDepartment(department)).Returns(true);
            var Result=_departmentController.Create(department) as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
    //     [Fact]
    //     public void CreateDepartment_ShouldReturnStatusCode400_WhenCreateThrowsValidationException()
    //     {
    //         Department department=new Department();
    //         _departmentService.Setup(obj=>obj.CreateDepartment(department)).Throws<ValidationException>();
    //         var Result=_departmentController.Create(department) as ObjectResult;
    //         Assert.Equal(400,Result?.StatusCode);
    //     }
    //     [Fact]
    //     public void CreateOrganisation_ShouldReturnStatusCode400_WhenCreateThrowsException()
    //     {
    //         Department department=new Department();
    //         _departmentService.Setup(obj=>obj.CreateDepartment(department)).Throws<Exception>();
    //         var Result=_departmentController.Create(department) as ObjectResult;
    //         Assert.Equal(400,Result?.StatusCode);
    //     }
    //     [Theory]
    //     [InlineData(null)]
    //     public void UpdateDepartment_ShouldReturnStatusCode400_WhenUpdateDepartmentIsNull(Department department)
    //     {
    //         var Result=_departmentController.Update(department) as ObjectResult;
    //         Assert.Equal(400,Result?.StatusCode);

    // }
    }
}
