using CustomerService.Services.CardServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllCards()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await _cardService.GetAllCards());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Authorize("Bearer")]
        [HttpGet("/api/cards/{id}")]
        public async Task<ActionResult<Client>> GetCardById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var singleCard = await _cardService.GetCardById(id);

                if (singleCard is null)
                    return NotFound();
                else
                    return Ok(singleCard);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<List<Card>>> AddCards([FromBody] Card _card)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Card.IsValidCard(_card.CardNumber))
                return BadRequest("Invalid card number");

            if (!Card.IsValidLimit(_card))
                return BadRequest("Invalid limit");

            try
            {
                var newCard = await _cardService.AddCards(_card);

                if (newCard is null)
                    return NotFound();
                else
                    return Ok(newCard);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Authorize("Bearer")]
        [HttpPut("/api/cards/{id}")]
        public async Task<ActionResult<List<Card>>> UpdateCard([FromBody] Card request, int id)
        {
            if (!Card.IsValidCard(request.CardNumber))
                return BadRequest("Invalid card number");

            if (!Card.IsValidLimit(request))
                return BadRequest("Invalid limit");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedCard = await _cardService.UpdateCard(request, id);

                if (updatedCard is null)
                    return NotFound();

                return Ok(updatedCard);
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Authorize("Bearer")]
        [HttpDelete("/api/cards/{id}")]
        public async Task<ActionResult<List<Card>>> DeleteCard(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var deletedCard = await _cardService.DeleteCard(id);

                if (deletedCard is null)
                    return NotFound();

                return Ok(deletedCard);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Authorize("Bearer")]
        [HttpGet("/api/{cardNumber}")]
        public async Task<ActionResult<int>> GetCardIdByCardNumber(string cardNumber)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!Card.IsValidCard(cardNumber)) return BadRequest("Invalid card number");
            try
            {
                var updatedCard = await _cardService.GetCardIdByCardNumber(cardNumber);

                if (updatedCard is null)
                    return NotFound();

                return Ok(updatedCard.Id);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Authorize("Bearer")]
        [HttpGet("/api/card/{id}/subtract/{value}")]
        public async Task<ActionResult<bool>> Subtract(int id, float value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (value < 0) return BadRequest("Invalid value");

            try
            {
                var result = await _cardService.Subtract(id, value);
                return Ok(result);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize("Bearer")]
        [HttpGet("/api/card/user/{cardNumber}")]
        public async Task<ActionResult<bool>> RetornaClientConciliado(string cardNumber)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!Card.IsValidCard(cardNumber)) return BadRequest("Invalid card number");

            try
            {
                var result = await _cardService.RetornaClientConciliado(cardNumber);
                return Ok(result);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
    }
}