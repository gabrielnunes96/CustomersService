using CustomerIssuer.Services.TransactionServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerIssuer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        /// <summary>
        /// Retorna todas as transações no BD
        /// </summary>
        /// <returns>Mensagem confirmando todas as transações</returns>
        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetAllTransactions()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await _transactionService.GetAllTransactions());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        /// <summary>
        /// Retorna uma transação específica no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mensagem com a transação específicada através do ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var singleTransaction = await _transactionService.GetTransactionById(id);

                if (singleTransaction is null)
                    return NotFound();
                else
                    return Ok(singleTransaction);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        /// <summary>
        /// Adicionar uma transação ao Banco de Dados
        /// </summary>
        /// <param name="_transaction"></param>
        /// <returns>Retorna mensagem contendo a transação no BD</returns>
        [HttpPost]
        public async Task<ActionResult<List<Transaction>>> AddTransaction(Transaction _transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Transaction.isValidValue(_transaction.TransactionValue)) 
                return BadRequest("Invalid Value");

            if (!Transaction.IsValidCard(_transaction.TransactionCardNumber))
                return BadRequest("Invalid card number");

            try
            {
                var newTransaction = await _transactionService.AddTransaction(_transaction);

                if (newTransaction is null)
                    return NotFound();
                else
                    return Ok(newTransaction);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        /// <summary>
        /// Atualiza determinada transação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns>Retorna mensagem contendo a transação atualizada através dos parâmetros "request" e "id"</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Transaction>>> UpdateTransaction(Transaction request, Guid id)
        {
            if (!Transaction.IsValidCard(request.TransactionCardNumber))
                return BadRequest("Invalid card number");

            if (!Transaction.isValidValue(request.TransactionValue))
                return BadRequest("Invalid Value");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedCard = await _transactionService.UpdateTransaction(request, id);

                if (updatedCard is null)
                    return NotFound();

                return Ok(updatedCard);
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        /// <summary>
        /// Deleta determinada transação do BD
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna mensagem contendo transação deletada</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Transaction>>> DeleteTransaction(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var deletedTransaction = await _transactionService.DeleteTransaction(id);

                if (deletedTransaction is null)
                    return NotFound();

                return Ok(deletedTransaction);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
