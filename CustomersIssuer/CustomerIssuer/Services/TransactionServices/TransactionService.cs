using CustomerIssuer.Data.Context;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace CustomerIssuer.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _dbContext;
        private readonly HttpClient _httpClient;


        public TransactionService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Transaction> GetTransactionById(int id)
        {
            var uniqueTransaction = await _dbContext.Transactions.FindAsync(id);
            if (uniqueTransaction is null) return null;
            return uniqueTransaction;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> AddTransaction(Transaction _transaction)
        {

            _dbContext.Transactions.Add(_transaction);
            _transaction.TransactionDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Transactions.FindAsync(_transaction.TransactionId);
        }

        public async Task<Transaction> UpdateTransaction(Transaction request, int id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction is null) return null;

            transaction.TransactionValue = request.TransactionValue;
            transaction.TransactionCardNumber = request.TransactionCardNumber;
            transaction.TransactionDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            transaction.TransactionId = Guid.NewGuid();
            transaction.TransactionApprovalId = Guid.NewGuid();
            
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Transactions.FindAsync(transaction.TransactionId);
        }

        public async Task<List<Transaction>> DeleteTransaction(int id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction is null) return null;

            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Transactions.ToListAsync();
        }
    }

}
