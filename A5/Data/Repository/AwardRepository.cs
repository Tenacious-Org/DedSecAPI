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
                award.RequesterId=employee!.Id;
                award.ApproverId = (int?)employee.ReportingPersonId;
                award.HRId= (int?)employee.HRId;
                award.StatusId=1;
                award.AddedBy=employee.Id;
                award.AddedOn=DateTime.Now;
                _context.SaveChanges();
                result=true;
                return result;
        }
        catch(Exception)
        {
            throw;
        }
    }
    public bool ApproveRequest(Award award,int id)
    {
         bool result = false;
       try{
         var employee = _master.GetEmployeeById(id);
                _context.Set<Award>().Update(award);
                award.UpdatedBy=employee?.Id;
                award.UpdatedOn=DateTime.Now;
                _context.SaveChanges();
                result=true;
                return result;  
       }
       catch(Exception)
       {
        throw;
       }
    }
   
     public Award? GetAwardById(int id)
        {
            try{
                var award=_context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .FirstOrDefault(nameof=> nameof.Id == id);
                return award;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool AddComments(Comment comment)
        {
           
            bool result=false;
            try{
                  _context.Set<Comment>().Add(comment);
                    _context.SaveChanges();
                    result=true;
                    return result;
            }
            catch(Exception)
            {
                throw;
            }

        }
        
         public IEnumerable<Comment> GetComments(int awardId)
        {
           try
           {
               var comments= _context.Set<Comment>()
                    .Include("Employees")
                    .Include("Awards")
                    .Where(nameof=>nameof.AwardId==awardId)
                    .ToList();
               return comments;
           }
           catch(Exception){
               throw;
           }
        }
          public IEnumerable<Award> GetAllAwardsList()
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .ToList();
                return award;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public IEnumerable<Award> GetAllWinners()
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch(Exception)
            {
                throw;
            }
        }
        
        public IEnumerable<Award> GetAllbyOrgwise(int id)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.OrganisationId == id)
                    .ToList();
                return award;
            }
            catch(Exception)
            {
                throw;
            }
        }

        
       
   }
}