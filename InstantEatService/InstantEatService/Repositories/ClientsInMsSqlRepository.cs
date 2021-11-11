using InstantEatService.Dto;
using InstantEatService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public class ClientsInMsSqlRepository : IDisposable, IClientsRepository
    {
        private readonly InstantEatDbContext _db;
        private readonly ILogger<ClientsInMsSqlRepository> _logger;

        private void Logging(string methodName)
        {
            _logger.LogTrace($"{methodName}is worked out (ClientsInMSSQLRepository)");
        }
        private void Logging(string methodName, string param)
        {
            _logger.LogTrace($"{methodName}({param})is worked out (ClientsInMSSQLRepository)");
        }
        private void Logging(string methodName, string param1,string param2)
        {
            _logger.LogTrace($"{methodName}({param1}, {param2}) is worked out (ClientsInMSSQLRepository)");
        }
        private void ThrowException(string paramName)
        {
            throw new NullReferenceException($"{nameof(paramName)} is empty or null");
        }

        public ClientsInMsSqlRepository(InstantEatDbContext db, ILogger<ClientsInMsSqlRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<Client>> GetAllClients()
        {
            await Task.CompletedTask;
            Logging(nameof(GetAllClients));

            return _db.Clients.ToList();
        }

        public async Task<Client> GetClient(int id)
        {
            Logging(nameof(GetClient), nameof(id));

            if (id == 0) ThrowException(nameof(id));

            return await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<Client> CreateClient(ClientCreateDto clientCreateDto)
        {
            Logging(nameof(CreateClient), nameof(clientCreateDto));

            if (clientCreateDto == null) ThrowException(nameof(clientCreateDto));

            var newClient = clientCreateDto.ToEntity();

            await _db.Clients.AddAsync(newClient);
            await _db.SaveChangesAsync();

            return newClient;
        }

        public async Task<bool> UpdateClient(int id, ClientCreateDto clientCreateDto)
        {
            Logging(nameof(UpdateClient), nameof(id), nameof(clientCreateDto));

            if (clientCreateDto == null) ThrowException(nameof(clientCreateDto));
            if (id == 0) ThrowException(nameof(id));
            var client = await GetClient(id);
            if (client == null) return false;
            client.Name = clientCreateDto.Name;
            client.PhoneNumber = clientCreateDto.PhoneNumber;
            _db.Update(client);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateClientName(string phoneNumber, string name)
        {
            Logging(nameof(UpdateClientName), nameof(phoneNumber), nameof(name));

            if (phoneNumber == null) ThrowException(nameof(phoneNumber));
            if (name == null) ThrowException(nameof(name));

            var client = await GetClientByPhoneNumber(phoneNumber);
            if (client == null) return false;
            client.Name = name;
            _db.Clients.Update(client);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClient(int id)
        {
            Logging(nameof(DeleteClient), nameof(id));

            if (id == 0) ThrowException(nameof(id));
            var client = await GetClient(id);
            if (client == null) return false;
            _db.Remove(client);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Client> GetClientByPhoneNumber(string number)
        {
            Logging(nameof(GetClientByPhoneNumber), nameof(number));
            var clients = await GetAllClients();
            return clients.FirstOrDefault(c => c.PhoneNumber == number);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
