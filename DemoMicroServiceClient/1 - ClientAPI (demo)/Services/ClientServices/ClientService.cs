using ClientAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI.Services.ClientServices
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
            var _uniqueClient = await _dbContext.Clients.FindAsync(id);
            if (_uniqueClient is null)
                return null;

            return _uniqueClient;
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<List<Client>> AddClient(Client _client)
        {
            _dbContext.Clients.Add(_client);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Clients.ToListAsync();

        }

        public async Task<List<Client>?> DeleteClient(int id)
        {
            var _client = await _dbContext.Clients.FindAsync(id);
            if (_client is null) return null;

            _dbContext.Clients.Remove(_client);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<List<Client>?> UpdateClient(int id, Client request)
        {
            var _client = await _dbContext.Clients.FindAsync(id);
            if (_client is null) return null;


            _client._accountNumber = request._accountNumber;
            _client._agencyNumber = request._agencyNumber;
            _client._userName = request._userName;
            _client._accountType = request._accountType;
            _client._idNumber = request._idNumber;
            _client._isActive = request._isActive;
            _client._accountType = request._accountType;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Clients.ToListAsync();

        }
    }
}
