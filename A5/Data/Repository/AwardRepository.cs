using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Models;
using Microsoft.EntityFrameworkCore;
namespace A5.Data.Repository
{
   public class AwardRepository
   {
     private readonly AppDbContext _context;
        private readonly MasterRepository _master;

        public AwardRepository(AppDbContext context, MasterRepository master)
        {
            _context=context;
            _master = master;
           
        }
    public bool RaiseAwardRequest(Award award,int id)
    {
        bool result=false;
        try{
            var employee = _master.GetEmployeeById(id);
                _context.Set<Award>().Add(award);
                award.RequesterId=employee.Id;
                award.ApproverId= (int)employee.ReportingPersonId;
                award.HRId= (int)employee.HRId;
                award.StatusId=1;
                award.AddedBy=employee.Id;
                award.AddedOn=DateTime.Now;
                _context.SaveChanges();
                result=true;
                return result;
        }
        catch(Exception exception)
        {
            throw exception;
        }
    }
   }
}