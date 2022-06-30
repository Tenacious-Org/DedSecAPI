using A5.Models;
using A5.Data.Base;
namespace A5.Data.Service.Interfaces
{
    public interface ITokenService {
        public  object GenerateToken(Login Credentials);

    }
        
    
}