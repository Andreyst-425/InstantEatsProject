using InstantEatService.Dto;
using InstantEatService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InstantEatService.Controllers
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
        /// Получить список всех клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClientDto>> GetAll()
        {
            var clients = await _clientService.GetAllClients();
            return clients.Select(c => new ClientDto(c));
        }

        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id"> идентификатор клиента </param>
        /// <returns></returns>
        [HttpGet("clients/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> GetClientById(int id)
        {
            var client = await _clientService.GetClient(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(new ClientDto(client));
        }

        /// <summary>
        /// Получить клиента по номеру телефона
        /// </summary>
        /// <param name="phoneNumber"> номер телефона клиента </param>
        /// <returns></returns>
        [HttpGet("{phoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> GetClientByPhoneNumber(string phoneNumber)
        {
            var client = await _clientService.GetClientByPhoneNumber(phoneNumber);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto(client));
        }


        /// <summary>
        /// Проверить клиента на наличие
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("checkExistClientResponse")]
        public async Task<bool> CheckClientOnExistance(string phoneNumber)
        {
            var isExisted = await _clientService.CheckClientOnExistance(phoneNumber);
            return isExisted;
        }

        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <returns></returns>
        [HttpPost("post")]
        public async Task<bool> AddClient([FromBody] ClientCreateDto clientCreateDto)
        {
            var isAdded = await _clientService.AddClient(clientCreateDto);

            return isAdded;
        }
        
        /// <summary>
        /// Изменить имя клиента
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        public async Task<bool> EditClientName()
        {
            var phoneNumber = Request.Query.FirstOrDefault(p => p.Key == "phoneNumber").Value;
            var name = Request.Query.FirstOrDefault(p => p.Key == "name").Value;

            var isPacthed = await _clientService.UpdateClientName(phoneNumber.ToString(), name.ToString());

            return isPacthed;
        }

        /// <summary>
        /// Обновить информацию о клиенте по id
        /// </summary>
        /// <param name="id"> идентификатор клиента </param>
        /// <param name="clientCreateDto"> новая информация о клиенте </param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateClient(int id, [FromBody] ClientCreateDto clientCreateDto)
        {
            var isUpdated = await _clientService.UpdateClient(id, clientCreateDto);
            return isUpdated ? Ok() : NotFound();
        }

        /// <summary>
        /// Удалить клиента по id
        /// </summary>
        /// <param name="id"> идентификатор клиента </param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var isDeleted = await _clientService.DeleteClient(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
