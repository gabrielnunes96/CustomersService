using CustomerIssuer.Data.Context;
using Newtonsoft.Json;
using System.Data;
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
        public async Task<Transaction> GetTransactionById(Guid id)
        {
            var result = await _dbContext.Transactions.FindAsync(id);
            if (result is null) return null;
            return result;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> AddTransaction(Transaction _transaction)
        {
            HttpClient client = new();

            var cardNumber = _transaction.TransactionCardNumber;
    
            //get jwt token
            var getDadosLoginUrl = await client.GetAsync($"http://localhost:5254/api/getacesso/{cardNumber}");
            getDadosLoginUrl.EnsureSuccessStatusCode();

            var dadosLoginContent = await getDadosLoginUrl.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(dadosLoginContent);

            string agency = result.agencyNumber;
            string account = result.accountNumber;

            var loginApiUrl = await client.GetAsync($"http://localhost:5254/api/login/{agency}/{account}");
            loginApiUrl.EnsureSuccessStatusCode();

            var loginContent = await loginApiUrl.Content.ReadAsStringAsync();
            var loginResult = JsonConvert.DeserializeObject<dynamic> (loginContent);
            string token = loginResult.acessToken;


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Substring("Bearer ".Length));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _transaction.TransactionDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            if(_transaction.TransactionApprovalId == Guid.Empty)
            {
                _transaction.TransactionId = Guid.NewGuid();
                _transaction.TransactionApprovalId = Guid.NewGuid();
            }

            var creditCardApiUrl = $"http://localhost:5254/api/{cardNumber}";
            var creditCardResponse = await client.GetAsync(creditCardApiUrl);
            var creditCardContent = await creditCardResponse.Content.ReadAsStringAsync();

            var creditCardId = Convert.ToInt64(creditCardContent);
            var value = _transaction.TransactionValue;

            var creditCardSubtractUrl = $"http://localhost:5254/api/card/{creditCardId}/subtract/{value}";
            var creditCardSubtractResponse = await client.GetAsync(creditCardSubtractUrl);

            await creditCardSubtractResponse.Content.ReadAsStringAsync();

            _dbContext.Transactions.Add(_transaction);

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Transactions.FindAsync(_transaction.TransactionId);
        }

        public async Task<Transaction> UpdateTransaction(Transaction request, Guid id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var transaction = await _dbContext.Transactions.FindAsync(id);

            var cardNumber = request.TransactionCardNumber;
            if (request.TransactionCardNumber == null)
                cardNumber = transaction.TransactionCardNumber;

            //get jwt token
            var getDadosLoginUrl = await client.GetAsync($"http://localhost:5254/api/getacesso/{cardNumber}");
            getDadosLoginUrl.EnsureSuccessStatusCode();

            var dadosLoginContent = await getDadosLoginUrl.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(dadosLoginContent);

            string agency = result.agencyNumber;
            string account = result.accountNumber;

            var loginApiUrl = await client.GetAsync($"http://localhost:5254/api/login/{agency}/{account}");
            loginApiUrl.EnsureSuccessStatusCode();

            var loginContent = await loginApiUrl.Content.ReadAsStringAsync();
            var loginResult = JsonConvert.DeserializeObject<dynamic>(loginContent);
            string token = loginResult.acessToken;


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Substring("Bearer ".Length));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            transaction.TransactionValue = request.TransactionValue;
            transaction.TransactionCardNumber = request.TransactionCardNumber;
            transaction.TransactionDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            transaction.TransactionApprovalId = Guid.NewGuid();

            var creditCardApiUrl = $"http://localhost:5254/api/{cardNumber}";
            var creditCardResponse = await client.GetAsync(creditCardApiUrl);
            var creditCardContent = await creditCardResponse.Content.ReadAsStringAsync();

            var creditCardId = Convert.ToInt64(creditCardContent);
            var value = request.TransactionValue;

            var creditCardSubtractUrl = $"http://localhost:5254/api/card/{creditCardId}/subtract/{value}";
            var creditCardSubtractResponse = await client.GetAsync(creditCardSubtractUrl);

            await creditCardSubtractResponse.Content.ReadAsStringAsync();
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Transactions.FindAsync(transaction.TransactionId);
        }

        public async Task<List<Transaction>> DeleteTransaction(Guid id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction is null) return null;

            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Transactions.ToListAsync();
        }
    }

}
