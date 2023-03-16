namespace CustomerIssuer.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionById(Guid id);
        Task<List<Transaction>> GetAllTransactions();
        Task<Transaction> AddTransaction(Transaction _transaction);
        Task<Transaction> UpdateTransaction(Transaction request, Guid id);
        Task<List<Transaction>> DeleteTransaction(Guid id);
    }
}
