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
        public bool ApproveOrReject(Award award,int id,bool result)
        {
            try{
                if(id==award.Id)
                {
                    if(result==true)
                    {
                        Approve(award,id);
                        result=true;
                        return result;
                    }
                    else{
                         Reject(award,id,reason);
                         result=false;
                         return result;

                    }
                  
                }                         
            }
            catch(exception exception)
            {
                throw exception;
            }
        }
        public bool Approve(Award award,int id)
        {
            bool result = false;
            try{
                var approve=_context.Set<Award>().FirstOrDefault(nameof=>nameof.Id==id);
                  approve.StatusId=2;
                  _context.SaveChanges();
                   result=true;
                    return result;
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return result;
        }
        public bool Reject(Award award,int id,string reason)
        {
             bool result=false;
            try{
                var reject = _context.Set<Award>().FirstOrDefault(nameof =>nameof.Id == id); 
                reject.StatusId=3;
                reject.RejectedReason=reason;
                _context.SaveChanges();
                 result=true;
                    return result;
               
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return result;
        }
        public bool Publish(Award award,int id,string couponCode)
        {
            bool result=false;
            try{
                if(id==award.Id)
                {
                    var coupon=_context.Set<Award>().FirstOrDefault(nameof=>nameof.Id==id);
                    coupon.CouponCode=couponCode;
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
      
    }
}