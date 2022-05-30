using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data.Repository;
using A5.Data.Service.Interfaces;
using A5.Models;
using A5.Data.Service;
namespace A5.Data.Service
{
    public class AwardService:IAwardService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        private readonly EmployeeService _employeeService;
        public AwardService(AppDbContext context, MasterRepository master,EmployeeService employeeService)
        {
            _context=context;
            _master = master;
            _employeeService=employeeService;
        }
        public bool RaiseRequest(Award award,int id)
        {
            bool result=false;
            try{
               // GetApproverDetails(id);
                _context.Set<Award>().Add(award);
                _context.SaveChanges();
                result=true;
                return result;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        // public IEnumerable<Employee> GetApproverDetails(int id)
        // {
        //      try{
        //         if(id==Employee.Id)
        //         {

        //         }
        //     }
        //     catch(Exception exception)
        //     {
        //         throw exception;
        //     }
        // }

        public bool Approval(Award award)
        {
             bool result = false;
            try{
                _context.Set<Award>().Update(award);
                _context.SaveChanges();
                result=true;
                return result;              
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        // public IEnumerable<Award> GetAwards(int ? pageId ,int ? employeeId)
        // {
          
        //     try
        //     {
        //         if(pageId==1) 
        //             return _context.Set<Award>().Where(nameof =>nameof.StatusId == 4 && nameof.AwardeeId==employeeId).ToList();
        //         else if(pageId==2) 
        //             return _context.Set<Award>().Where(nameof => nameof.RequesterId == employeeId).ToList().OrderBy(nameof => nameof.StatusId);
        //         else if(pageId==3) 
        //             return _context.Set<Award>().Where(nameof => nameof.ApproverId == employeeId).ToList().OrderBy(nameof => nameof.StatusId);
        //         else if(pageId==4) 
        //             return _context.Set<Award>().Where(nameof => nameof.HRId == employeeId && (nameof.StatusId == 2 || nameof.StatusId == 4)).ToList().OrderBy(nameof => nameof.StatusId);
        //         else
        //             return _context.Set<Award>().Where(nameof =>nameof.StatusId == 4).ToList();
        //     }
        //     catch(Exception exception)
        //     {
        //         throw exception;
        //     }
        // }
        
        public IEnumerable<Award> GetRequestedAward(int employeeId)
        {
            try{
                return _context.Set<Award>().Where(nameof=> nameof.Awardee.Id == employeeId && nameof.StatusId == 3).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public object GetAwardById(int id)
        {
            try{
                var award= _master.GetAwardById(id);
                return  new{
                    id = award.Id,
                    awardeeName = award.Awardee.FirstName,
                    requesterName = award.Awardee.ReportingPerson.FirstName,
                    approverName = award.Awardee.ReportingPerson.ReportingPerson.FirstName,
                    hRName = award.Awardee.HR.FirstName,
                    status=award.Status.StatusName,
                    award=award.AwardType.AwardName,
                    awardImage=award.AwardType.Image,
                    reason=award.Reason,
                    rejectedReason=award.RejectedReason,
                    designation=award.Awardee.Designation.DesignationName,
                    department=award.Awardee.Designation.Department.DepartmentName,
                    organisation=award.Awardee.Designation.Department.Organisation.OrganisationName

                };
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }


        public bool AddComment(Comment comment)
        {
            bool result = false;
            try{
                    _context.Set<Comment>().Add(comment);
                    _context.SaveChanges();
                    result=true;
                    return result;
            }
            catch(Exception exception){
                throw exception;
            }
           
        }
        public IEnumerable<object> GetComments(int awardId)
        {
           try
           {
               var comments=_master.GetComments(awardId);
               return comments.Select( Comment =>  new{
                   id=Comment.Id,
                   employeeName=Comment.Employees.FirstName,
                   employeeImage=Comment.Employees.Image
               });
           }
           catch(Exception exception){
               throw exception;
           }
        }

        public IEnumerable<object> GetAwardsList(int ? pageId,int ? employeeId)
        {
            
            try
            {
                var awards = _master.GetAllAwardsList();
                if(pageId==1) 
                    awards =awards.Where(nameof =>nameof.StatusId == 4 && nameof.AwardeeId==employeeId).ToList();
                else if(pageId==2) 
                    awards =awards.Where(nameof => nameof.RequesterId == employeeId).ToList().OrderBy(nameof => nameof.StatusId);
                else if(pageId==3) 
                    awards =awards.Where(nameof => nameof.ApproverId == employeeId).ToList().OrderBy(nameof => nameof.StatusId);
                else if(pageId==4) 
                    awards =awards.Where(nameof => nameof.HRId == employeeId && (nameof.StatusId == 2 || nameof.StatusId == 4)).ToList().OrderBy(nameof => nameof.StatusId);
                else
                    awards =awards.Where(nameof =>nameof.StatusId == 4).ToList();
                return awards.Select( Award => new{
                    id = Award.Id,
                    awardeeName = Award.Awardee.FirstName,
                    requesterName = Award.Awardee.ReportingPerson.FirstName,
                    approverName = Award.Awardee.ReportingPerson.ReportingPerson.FirstName,
                    hRName = Award.Awardee.HR.FirstName,
                    status=Award.Status.StatusName,
                    awardName=Award.AwardType.AwardName,
                    awardImage=Award.AwardType.Image,
                    reason=Award.Reason,
                    rejectedReason=Award.RejectedReason,
                    designation=Award.Awardee.Designation.DesignationName,
                    department=Award.Awardee.Designation.Department.DepartmentName,
                    organisation=Award.Awardee.Designation.Department.Organisation.OrganisationName                 
                });
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

    }
}