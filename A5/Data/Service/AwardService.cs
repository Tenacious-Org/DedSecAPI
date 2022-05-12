using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Data.Service.Interfaces;
using A5.Models;

namespace A5.Data.Service
{
    public class AwardService:IAwardService
    {
        private readonly AppDbContext _context;
        public AwardService(AppDbContext context)
        {
            _context=context;
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
        public bool Approve(Award award,int id)
        {
            bool result = false;
            try{
                var approve = _context.Set<Award>().FirstOrDefault(nameof=>nameof.Id==id);
                  approve.StatusId = 2;
                  _context.SaveChanges();
                   result=true;
                    return result;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public bool Reject(Award award,int id)
        {
             bool result = false;
            try{
                _context.Set<Award>().Update(award);
                var reject = _context.Set<Award>().FirstOrDefault(nameof=>nameof.Id==id);
                reject.StatusId = 3;
                _context.SaveChanges();
                result=true;
                return result;
               
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        
        public bool Publish(Award award,int id)
        {
            bool result=false;
            try{
                if(id == award.Id)
                {
                    _context.Set<Award>().Update(award);
                    var coupon = _context.Set<Award>().FirstOrDefault(nameof=>nameof.Id==id);
                    coupon.StatusId=4;
                    _context.SaveChanges();
                    result=true;
                    return result;

                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return result;
        }
        public IEnumerable<Award> GetAwardsByStatus(int id)
        {
          
            try{
                return _context.Set<Award>().Where(nameof =>nameof.StatusId == id).ToList();
                
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Award> GetMyAwards(int employeeId)
        {
            try{
            
        
                    return _context.Set<Award>().Where(nameof =>nameof.StatusId == 4 && nameof.AwardeeId==employeeId).ToList();  
                
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Award> GetRequestedAward(int employeeId)
        {
            try{
                return _context.Set<Award>().Where(nameof=>nameof.Employee.Id==employeeId && nameof.StatusId==3).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public Award GetAward(int id)
        {
            try{
                return _context.Set<Award>().FirstOrDefault(nameof=>nameof.Id==id && nameof.StatusId==2);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public bool AddComment(Comment comment,int awardId,int currentUserId)
        {
            bool result=false;
            try{
                Award award=new Award();
                if(awardId==award.Id)
                {
                    _context.Set<Comment>().Add(comment);
                    _context.SaveChanges();
                    result=true;
                    return result;
                }
              
            }
            catch(Exception exception){
                throw exception;
            }
            return result;
        }
    }
}