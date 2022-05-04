namespace A5.Data.Base
{
    public interface IValidation<T> where T : class
    {
        bool Validation(T entity);
    }
}