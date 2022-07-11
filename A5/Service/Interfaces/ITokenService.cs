using A5.Models;
using A5.Data.Repository;
namespace A5.Service.Interfaces
{
    public interface ITokenService {
        public  object GenerateToken(Login Credentials);

    }
        
    
}