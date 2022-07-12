using A5.Models;
namespace A5.Hasher
{
    public static class PasswordHasherFactory
    {
        public static BCryptPasswordHasher<Employee> GetPasswordHasherFactory()
        {
            return new BCryptPasswordHasher<Employee>();

        }
    }
}