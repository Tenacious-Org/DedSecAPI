using A5.Models;
using System.Collections.Generic;
namespace Testing.MockData
{
    public class AwardMock
    {
       
        public static Award GetInvalidAward()
        {
            return new Award();
        }
        public static Award GetValidAward()
        {
            return new Award()
            {
                Id=1,
                RequesterId= 6,
                AwardeeId =7,
                AwardTypeId= 1,
                ApproverId= 5,
                HRId= 4,
                Reason= "Best Performer in Team",
                RejectedReason= null,
                CouponCode= "KJ7JH876HBH",
                StatusId= 4,
                AddedBy= 6,
                UpdatedBy= 4,
            };
        }
        public static List<Award> GetListOfAwards()
        {
            return new List<Award>
            {
                new Award(){ Id=1,RequesterId= 6, AwardeeId =7, AwardTypeId= 1,ApproverId= 5, HRId= 4,Reason= "Best Performer in Team",RejectedReason= null,CouponCode= "KJ7JH876HBH",StatusId= 4, AddedBy= 6,UpdatedBy= 4,},
                new Award(){ Id=2,RequesterId= 5, AwardeeId =6, AwardTypeId= 1,ApproverId= 5, HRId= 4,Reason= "Best Performer in Team",RejectedReason= null,CouponCode= "KJ7JH876HBH",StatusId= 4, AddedBy= 6,UpdatedBy= 4,},
                new Award(){ Id=3,RequesterId= 4, AwardeeId =5, AwardTypeId= 1,ApproverId= 5, HRId= 4,Reason= "Best Performer in Team",RejectedReason= null,CouponCode= "KJ7JH876HBH",StatusId= 4, AddedBy= 6,UpdatedBy= 4,},
                new Award(){ Id=4,RequesterId= 5, AwardeeId =7, AwardTypeId= 1,ApproverId= 5, HRId= 4,Reason= "Best Performer in Team",RejectedReason= null,CouponCode= "KJ7JH876HBH",StatusId= 4, AddedBy= 6,UpdatedBy= 4,},
                new Award(){ Id=5,RequesterId= 6, AwardeeId =7, AwardTypeId= 1,ApproverId= 5, HRId= 4,Reason= "Best Performer in Team",RejectedReason= null,CouponCode= "KJ7JH876HBH",StatusId= 4, AddedBy= 6,UpdatedBy= 4,}
            };
        }
        public static Award GetAwardsById(int Id)
        {
            return new Award()
            {
                Id=1,
                RequesterId= 6,
                AwardeeId =7,
                AwardTypeId= 1,
                ApproverId= 5,
                HRId= 4,
                Reason= "Best Performer in Team",
                RejectedReason= null,
                CouponCode= "KJ7JH876HBH",
                StatusId= 4,
                AddedBy= 6,
                UpdatedBy= 4,
            };
        }
       public static IEnumerable<object> GetComment()
       {
            yield return new Comment
            {
                Id=1,
                Comments="Congradulations",
                EmployeeId=8,
                AwardId=2,

            };
        }
    }
}
 