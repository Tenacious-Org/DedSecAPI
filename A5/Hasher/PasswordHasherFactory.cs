using A5.Models;
namespace A5.Hasher
{
    public class PasswordHasherFactory
    {
        public static BCryptPasswordHasher<Employee> GetPasswordHasherFactory()
        {
            return new BCryptPasswordHasher<Employee>();

        }
    }
}