namespace CustomerService.Services.LoginServices
{
    public interface ILoginService
    {
        Task<object> FindByLogin(string agency, string account);
    }
}
