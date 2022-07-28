using A5.Models;
using A5.Data.Repository;
namespace A5.Service.Interfaces
{
    public interface ITokenService
    {
         object GenerateToken(Login Credentials);

    }


}