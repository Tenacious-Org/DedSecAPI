namespace A5.Data.Base
{
    public interface IValidation<T> where T : class
    {
        bool CreateValidation(T entity);
        bool ValidateGetById(int id);
        
    }
}