using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;

namespace A5.Data.Service
{
    public class AwardService 
    {
        private readonly AppDbContext _context;
        public AwardService(AppDbContext context)
        { 
            _context = context;
        }
        public bool RaiseRequest(Award award){
            return true;
        }
        public Award GetById(int id){
             try
            {
                return _context.Set<Award>().FirstOrDefault(nameof =>nameof.Id == id);

            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Award> GetAllAwards(){
            try
            {
                return _context.Set<Award>().Where(nameof =>nameof.StatusId == 4).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }

        }

        public IEnumerable<Award> GetAllRequester(int id){
            try
            {
                return _context.Set<Award>().Where(nameof =>nameof.RequesterId==id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }

        }
        public IEnumerable<Award> GetAllApprover(int id){
            try
            {
                return _context.Set<Award>().Where(nameof =>nameof.ApproverId==id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }

        }
        public IEnumerable<Award> GetAllPublisher(int id){
            try
            {
                return _context.Set<Award>().Where(nameof =>nameof.HRId==id && nameof.StatusId==2).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }

        }







    }
}