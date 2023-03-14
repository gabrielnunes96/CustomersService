using CustomerService.Services.ClientServices;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Endpoint para buscar todos os clientes
        /// </summary>
        /// <param name=""></param>
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllClients()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            try
            {
                return Ok(await _clientService.GetAllClients());
            }
            catch(ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
            
        }
        /// <summary>
        /// Endpoint para buscar um cliente
        /// </summary>
        /// <param name="id"></param>
        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var singleClient = await _clientService.GetClientById(id);

                if (singleClient is null)
                    return NotFound();
                else
                    return Ok(singleClient);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        /// <summary>
        /// Endpoint para adicionar um cliente
        /// </summary>
        /// <param name="_client"></param>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<List<Client>>> AddClient(Client _client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newClient = await _clientService.AddClient(_client);

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
        /// <summary>
        /// Endpoint para atualizar um cliente
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Client>>> UpdateClient(int id, Client request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedClient = await _clientService.UpdateClient(id, request);

                if (updatedClient is null)
                    return NotFound();

                return Ok(updatedClient);
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); 
            }
        }

        /// <summary>
        /// Endpoint para deletar um cliente
        /// </summary>
        /// <param name="id"></param>
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Client>>> DeleteClient(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var deletedClient = await _clientService.DeleteClient(id);

                if (deletedClient is null)
                    return NotFound();

                return Ok(deletedClient);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }

}
