using CustomerIssuer.Data.Context;
using System.Net.Http.Headers;
namespace CustomerIssuer.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _dbContext;

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
            HttpClient client = new HttpClient();
            Uri usuarioUri;
            if (client is null)
            {
                client.BaseAddress = new Uri("http://localhost:5254/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            _dbContext.Transactions.Add(_transaction);
            _transaction.TransactionDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _transaction.TransactionId = Guid.NewGuid();
            _transaction.TransactionApprovalId = Guid.NewGuid();

            var cardNumber = _transaction.TransactionCardNumber;
            var creditCardApiUrl = $"http://localhost:5254/api/{cardNumber}";
            var creditCardResponse = await client.GetAsync(creditCardApiUrl);
            var creditCardContent = await creditCardResponse.Content.ReadAsStringAsync();

            var creditCardId = Convert.ToInt64(creditCardContent);
            var value = _transaction.TransactionValue;

            var creditCardSubtractUrl = $"http://localhost:5254/api/card/{creditCardId}/subtract/{value}";
            var creditCardSubtractResponse = await client.GetAsync(creditCardSubtractUrl);

            await creditCardSubtractResponse.Content.ReadAsStringAsync();

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
