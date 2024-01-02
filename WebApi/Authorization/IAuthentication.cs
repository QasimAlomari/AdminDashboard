namespace WebApi.Authorization
{
    public interface IAuthentication<T>
    {
        string GetJsonWebToken(T entity);
    }
}
