using InstantEatService.Models;
using InstantEatService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class ClientService
    {
        private readonly IClientsRepository _clients;

        public ClientService(IClientsRepository clients)
        {
            _clients = clients;
        }
        public async Task<Client> GetClientByNumber(string number)
        {
            var clients = await _clients.GetAllClients();
            return clients.FirstOrDefault(c => c.PhoneNumber == number);
        }
    }
}
