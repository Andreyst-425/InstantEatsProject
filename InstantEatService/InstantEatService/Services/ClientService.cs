using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientsRepository _clients;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientsRepository clients, ILogger<ClientService> logger)
        {
            _clients = clients;
            _logger = logger;
        }
        private void Logging(string methodName)
        {
            _logger.LogTrace($"{methodName}is worked out (ClientService)");
        }

        private void Logging(string methodName, string param)
        {
            _logger.LogTrace($"{methodName}({param})is worked out (ClientService)");
        }

        private void Logging(string methodName, string param1, string param2)
        {
            _logger.LogTrace($"{methodName}({param1},{param2})is worked out (ClientService)");
        }

        public async Task<bool> AddClient(ClientCreateDto clientCreateDto)
        {
            Logging(nameof(AddClient), nameof(clientCreateDto));
            var client = await _clients.GetClientByPhoneNumber(clientCreateDto.PhoneNumber);
            if (client != null) return false;
            await _clients.CreateClient(clientCreateDto);
            return true;
        }

        public async Task<bool> EditClientName(string phoneNumber, string name)
        {
            Logging(nameof(EditClientName), nameof(phoneNumber), nameof(name));
            return await _clients.UpdateClientName(phoneNumber, name);
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            Logging(nameof(GetAllClients));
            return await _clients.GetAllClients();
        }

        public async Task<Client> GetClient(int id)
        {
            Logging(nameof(GetClient), nameof(id));
            return await _clients.GetClient(id);
        }

        public async Task<Client> GetClientByPhoneNumber(string number)
        {
            Logging(nameof(GetClientByPhoneNumber), nameof(number));
            return await _clients.GetClientByPhoneNumber(number);
        }

        public async Task<bool> UpdateClientName(string phoneNumber, string name)
        {
            Logging(nameof(UpdateClientName), nameof(phoneNumber),nameof(name));
            return await _clients.UpdateClientName(phoneNumber, name);
        }

        public async Task<bool> DeleteClient(int id)
        {
            Logging(nameof(DeleteClient), nameof(id));
            return await _clients.DeleteClient(id);
        }

        public async Task<bool> UpdateClient(int id, ClientCreateDto clientCreateDto)
        {
            Logging(nameof(UpdateClient), nameof(id),nameof(clientCreateDto));
            return await _clients.UpdateClient(id, clientCreateDto);
        }

        public async Task<bool> CheckClientOnExistance(string phoneNumber)
        {
            Logging(nameof(CheckClientOnExistance), nameof(phoneNumber));
            var client = await _clients.GetClientByPhoneNumber(phoneNumber);
            if (client == null) return false;
            return true;
        }
    }
}
