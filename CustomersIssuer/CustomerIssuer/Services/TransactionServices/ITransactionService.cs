namespace CustomerIssuer.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionById(int id);
        Task<List<Transaction>> GetAllTransactions();
        Task<Transaction> AddTransaction(Transaction _transaction);
        Task<Transaction> UpdateTransaction(Transaction request, int id);
        Task<List<Transaction>> DeleteTransaction(int id);
    }
}
