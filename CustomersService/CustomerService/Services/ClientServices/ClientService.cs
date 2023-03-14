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

        public async Task<Client> GetClientById(int id)
        {
            var uniqueClient = await _dbContext.Clients.FindAsync(id);

            return uniqueClient;
        }
        public async Task<List<Client>> GetAllClients()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> AddClient(Client _client)
        {
            _dbContext.Clients.Add(_client);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Clients.FindAsync(_client.Id);
        }

        public async Task<List<Client>> DeleteClient(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> UpdateClient(int id, Client request)
        {
            var client = await _dbContext.Clients.FindAsync(id);

            client.UserName = client.UserName;
            client.AccountNumber = request.AccountNumber;
            client.AgencyNumber = request.AgencyNumber;
            client.IsActive = request.IsActive;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Clients.FindAsync(client.Id);
        }
        public async Task<Client> ClientLogin(string agency, string account)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(x => x.AgencyNumber.Equals(agency) && x.AccountNumber.Equals(account));
        }
    }
}
