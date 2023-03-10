using CustomerService.Services.CardServices;
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

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllCards()
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                return Ok(await _cardService.GetAllCards());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetCardById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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

        [HttpPost]
        public async Task<ActionResult<List<Card>>> AddCards(Card _card)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newClient = await _cardService.AddCards(_card);

                if (newClient is null)
                    return NotFound();
                else
                    return Ok(newClient);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Card>>> UpdateCard(Card request, int id)
        {
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

        [HttpDelete("{id}")]
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
    }
}
