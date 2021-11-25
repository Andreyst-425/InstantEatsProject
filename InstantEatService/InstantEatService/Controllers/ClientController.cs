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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<ClientDto>> GetAll()
        {
            var clients = await _clientService.GetAllClients();
            return clients.Select(c => new ClientDto(c)).ToList();
        }

        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id"> идентификатор клиента </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ClientDto> GetClientById(int id)
        {
            var client = await _clientService.GetClient(id);
            //проверка на null была в сервисе, так здесь все ок
            return new ClientDto(client);
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
        public async Task<ClientDto> GetClientByPhoneNumber(string phoneNumber)
        {
            var client = await _clientService.GetClientByPhoneNumber(phoneNumber);
            //проверка на null была в сервисе, так здесь все ок
            return new ClientDto(client);
        }

        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddClient()
        {
            var phoneNumber = Request.Query.FirstOrDefault(p => p.Key == "phoneNumber").Value;

            var isAdded = await _clientService.AddClient(phoneNumber.ToString());

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
        public async Task<bool> UpdateClient(int id, [FromBody] ClientCreateDto clientCreateDto)
        {
            var isUpdated = await _clientService.UpdateClient(id, clientCreateDto);
            return isUpdated;
        }

        /// <summary>
        /// Удалить клиента по id
        /// </summary>
        /// <param name="id"> идентификатор клиента </param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<bool> DeleteClient(int id)
        {
            var isDeleted = await _clientService.DeleteClient(id);
            return isDeleted;
        }
    }
}
