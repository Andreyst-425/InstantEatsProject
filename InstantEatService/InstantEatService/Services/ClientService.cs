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


    }
}
