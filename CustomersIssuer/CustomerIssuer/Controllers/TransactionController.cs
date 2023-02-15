using CustomerIssuer.Services.TransactionServices;
using Microsoft.AspNetCore.Cors.Infrastructure;
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

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetAllTransactions()
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                return Ok(await _transactionService.GetAllTransactions());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
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

        [HttpPost]
        public async Task<ActionResult<List<Transaction>>> AddTransaction([FromBody] Transaction _transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Transaction>>> UpdateTransaction([FromBody] Transaction request, int id)
        {
            if (!Transaction.IsValidCard(request.TransactionCardNumber))
                return BadRequest("Invalid card number");

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

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Transaction>>> DeleteTransaction(int id)
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
