using InstantEatService.DtoModels;
using InstantEatService.Models;
using InstantEatService.Repositories;
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

        private readonly IClients _clients;

        public ClientController(IClients clients)
        {
            _clients = clients;
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
            await Task.CompletedTask;
            var clients = _clients.GetAllClients();
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
        public async Task<ActionResult<ClientDto>> GetClient(Guid id)
        {
            var client = await _clients.GetClient(id);

            if(client == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto(client));
        }

        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="clientCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> PostClient([FromBody] ClientCreateDto clientCreateDto)
        {
            var client = await _clients.CreateClient(clientCreateDto);
            return Ok(new ClientDto(client));
        }

        /// <summary>
        /// Обновить информацию о клиенте 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutClient(Guid id, string name, string phoneNumber)
        {
            var isUpdated = await _clients.UpdateClient(id, name, phoneNumber);
            return isUpdated ? Ok() : NotFound();
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(Guid id)
        {
            var isDeleted = await _clients.DeleteClient(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
