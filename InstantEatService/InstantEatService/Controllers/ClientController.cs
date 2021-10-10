using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Repositories;
using InstantEatService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InstantEatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientsRepository _clients;
        private readonly IClientService _clientService;

        public ClientController(IClientsRepository clients, IClientService clientService)
        {
            _clients = clients;
            _clientService = clientService;
        }

        /// <summary>
        /// Получить список всех клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClientDto>> GetAllClients()
        {
            var clients = await _clients.GetAllClients();
            return clients.Select(c => new ClientDto(c));
        }

        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var client = await _clients.GetClient(id);

            if(client == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto(client));
        }

        
        [HttpGet("{phoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> GetClientByPhoneNumber(string phoneNumber)
        {
            var client = await _clients.GetClientByPhoneNumber(phoneNumber);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto(client));
        }

        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> PostClient()
        {
            var phoneNumber = Request.Query.FirstOrDefault(p => p.Key == "phoneNumber").Value;

            var isPosted = await _clientService.PostClient(phoneNumber.ToString());

            return isPosted;
        }
        
        /// <summary>
        /// Изменить имя клиента
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        public async Task<bool> PatchClientName()
        {
            var phoneNumber = Request.Query.FirstOrDefault(p => p.Key == "phoneNumber").Value;
            var name = Request.Query.FirstOrDefault(p => p.Key == "name").Value;

            var isPacthed = await _clients.UpdateClientName(phoneNumber.ToString(), name.ToString());

            return isPacthed;
        }

        /// <summary>
        /// Обновить информацию о клиенте по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientCreateDto"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutClient(int id, [FromBody] ClientCreateDto clientCreateDto)
        {
            var isUpdated = await _clients.UpdateClient(id, clientCreateDto);
            return isUpdated ? Ok() : NotFound();
        }

        /// <summary>
        /// Удалить клиента по id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var isDeleted = await _clients.DeleteClient(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
