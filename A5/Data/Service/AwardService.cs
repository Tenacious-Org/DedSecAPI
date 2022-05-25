using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data.Repository;
using A5.Data.Service.Interfaces;
using A5.Models;

namespace A5.Data.Service
{
    public class AwardService:IAwardService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        public AwardService(AppDbContext context, MasterRepository master)
        {
            _context=context;
            _master = master;
        }
        public bool RaiseRequest(Award award)
        {
            bool result=false;
            try{
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
        public IEnumerable<Award> GetAwards(int ? pageId ,int ? employeeId)
        {
          
            try
            {
                if(pageId==1) 
                    return _context.Set<Award>().Where(nameof =>nameof.StatusId == 4 && nameof.AwardeeId==employeeId).ToList();
                else if(pageId==2) 
                    return _context.Set<Award>().Where(nameof => nameof.RequesterId == employeeId).ToList().OrderBy(nameof => nameof.StatusId);
                else if(pageId==3) 
                    return _context.Set<Award>().Where(nameof => nameof.ApproverId == employeeId).ToList().OrderBy(nameof => nameof.StatusId);
                else if(pageId==4) 
                    return _context.Set<Award>().Where(nameof => nameof.HRId == employeeId && (nameof.StatusId == 2 || nameof.StatusId == 4)).ToList().OrderBy(nameof => nameof.StatusId);
                else
                    return _context.Set<Award>().Where(nameof =>nameof.StatusId == 4).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        
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
        public Award GetAwardById(int id)
        {
            try{
                return _context.Set<Award>().FirstOrDefault(nameof=> nameof.Id == id);
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
        public IEnumerable<Comment> GetComments(int awardId)
        {
           try
           {
               return _context.Set<Comment>().Where(nameof=>nameof.AwardId==awardId).ToList();
           }
           catch(Exception exception){
               throw exception;
           }
        }

        public IEnumerable<object> GetAwardsList()
        {
            try
            {
                var award = _master.GetAllAwardsList();
                return award.Select( Award => new{
                    id = Award.Id,
                    awardeeName = Award.Awardee.FirstName,
                    awardName = Award.AwardType.AwardName,
                    award = Award.Status.StatusName
                });
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

    }
}