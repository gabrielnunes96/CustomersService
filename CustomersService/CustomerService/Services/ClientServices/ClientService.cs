using CustomerService.Data.Context;

namespace CustomerService.Services.ClientServices
{
    public class ClientService : IClientService
    {
        private readonly DataContext _dbContext;
        public ClientService(DataContext context)
        {
            _dbContext = context;
        }

        public async Task<Client?> GetClientById(int id)
        {
            var uniqueClient = await _dbContext.Clients.FindAsync(id);
            if (uniqueClient is null)
                return null;

            return uniqueClient;
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client?> AddClient(Client _client)
        {
            _dbContext.Clients.Add(_client);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Clients.FindAsync(_client.Id);
        }

        public async Task<List<Client>?> DeleteClient(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client is null) return null;

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client?> UpdateClient(int id, Client request)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client is null) return null;


            client.accountNumber = request.accountNumber;
            client.agencyNumber = request.agencyNumber;
            client.userName = request.userName;
            client.accountType = request.accountType;
            client.idNumber = request.idNumber;
            client.isActive = request.isActive;
            client.accountType = request.accountType;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Clients.FindAsync(client.Id);

        }
    }
}
