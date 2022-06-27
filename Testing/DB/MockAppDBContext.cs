using A5.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Testing.MockData;
namespace Testing.DB
{
    public class MockAppDBContext
    {
         public static AppDbContext GetInMemoryDbContext(){

        var Options= new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "A5_InMemoryDatabase").Options;
        return new AppDbContext(Options);
    

    }       public static void SeedMockDataInMemoryDb(AppDbContext dbContext)
        {
           dbContext.Awards.AddRange(AwardMock.GetListOfAwards());
            dbContext.SaveChanges();
        }
    }
}