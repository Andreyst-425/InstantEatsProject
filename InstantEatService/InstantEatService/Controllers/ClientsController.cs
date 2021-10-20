﻿using InstantEatService.Dto;
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
    public class ClientsController : ControllerBase
    {

        
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
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
        public async Task<IEnumerable<ClientDto>> GetAllClients()
        {
            var clients = await _clientService.GetAllClients();
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
            var client = await _clientService.GetClient(id);

            if(client == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto(client));
        }

        /// <summary>
        /// Получить данные о клиенте по номеру телефона
        /// </summary>
        /// <param name="phoneNumber"></param>
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

            var isPacthed = await _clientService.UpdateClientName(phoneNumber.ToString(), name.ToString());

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
            var isUpdated = await _clientService.UpdateClient(id, clientCreateDto);
            return isUpdated ? Ok() : NotFound();
        }

        /// <summary>
        /// Удалить клиента по id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var isDeleted = await _clientService.DeleteClient(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
