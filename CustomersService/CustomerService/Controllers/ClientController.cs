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
        /// <returns> Retorna mensagem com todos os clientes </returns>
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
        /// <returns> Retorna mensagem contendo o cliente específicado pelo ID </returns>
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
        /// <returns> Retorna mensagem com o Cliente Adicionado ao BD </returns>
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
        /// <returns> Retorna mensagem com o cliente atualizado </returns>
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
        /// <returns> Retorna mensagem com o cliente deletado </returns>
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
        /// <summary>
        /// Endpoint retorna se o cliente informado existe no BD 
        /// </summary>
        /// <param name="agency"></param>
        /// <param name="account"></param>
        /// <returns> Retorna mensagem contendo cliente encontrado</returns>
        [AllowAnonymous]
        [HttpGet("/api/{agency}/{account}")]
        public async Task<ActionResult<Client>> LoginClient(string agency, string account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _clientService.ClientLogin(agency, account);

                if (response is null)
                    return NotFound();

                return Ok(response);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }

}
