using CustomerService.Services.ClientServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllClients()
        {
            if (!ModelState.IsValid)
                return BadRequest();
                
            try
            {
                return Ok(await _clientService.GetAllClients());
            }
            catch(ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var _singleClient = await _clientService.GetClientById(id);

                if (_singleClient is null)
                    return NotFound();
                else
                    return Ok(_singleClient);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> AddClient(Client _client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var _newClient = await _clientService.AddClient(_client);

                if (_newClient is null)
                    return NotFound();
                else
                    return Ok(_newClient);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Client>>> UpdateClient(int id, Client request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var _updatedClient = await _clientService.UpdateClient(id, request);

                if (_updatedClient is null)
                    return NotFound();

                return Ok(_updatedClient);
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); 
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Client>>> DeleteClient(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var _deletedClient = await _clientService.DeleteClient(id);

                if (_deletedClient is null)
                    return NotFound();

                return Ok(_deletedClient);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }

}
