using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientsRepository _clients;

        public ClientService(IClientsRepository clients)
        {
            _clients = clients;
        }

        public async Task<bool> PostClient(string phoneNumber)
        {
            var client = await _clients.GetClientByPhoneNumber(phoneNumber);

            if (client != null) return false;

            var clientCreateDto = new ClientCreateDto() { PhoneNumber = phoneNumber, Name = null };

            await _clients.CreateClient(clientCreateDto);

            return true;
        }


        public async Task<bool> PatchClientName(string phoneNumber, string name)
        {
            return await _clients.UpdateClientName(phoneNumber, name);
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _clients.GetAllClients();
        }

        public async Task<Client> GetClient(int id)
        {
            return await _clients.GetClient(id);
        }

        public async Task<Client> GetClientByPhoneNumber(string number)
        {
            return await _clients.GetClientByPhoneNumber(number);
        }

        public async Task<bool> UpdateClientName(string phoneNumber, string name)
        {
            return await _clients.UpdateClientName(phoneNumber, name);
        }

        public async Task<bool> DeleteClient(int id)
        {
            return await _clients.DeleteClient(id);
        }

        public async Task<bool> UpdateClient(int id, ClientCreateDto clientCreateDto)
        {
            return await _clients.UpdateClient(id, clientCreateDto);
        }
    }
}
