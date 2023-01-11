namespace ClientAPI.Services.ClientServices
{
    public interface IClientService
    {
        Task<Client?> GetClientById(int id);
        Task<List<Client>> GetAllClients();
        Task<List<Client>> AddClient(Client _client);
        Task<List<Client>?> UpdateClient(int id, Client request);
        Task<List<Client>?> DeleteClient(int id);

    }
}
