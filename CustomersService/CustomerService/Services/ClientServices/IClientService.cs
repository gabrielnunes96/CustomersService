namespace CustomerService.Services.ClientServices
{
    public interface IClientService
    {
        Task<Client> GetClientById(int id);
        Task<List<Client>> GetAllClients();
        Task<Client> AddClient(Client _client);
        Task<Client> UpdateClient(int id, Client request);
        Task<List<Client>> DeleteClient(int id);
        Task<Client> ClientLogin(string agency, string account);

    }
}
