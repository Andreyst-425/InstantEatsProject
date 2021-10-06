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
            _logger.LogTrace($"{methodName}is worked out");
        }
        private void Logging(string methodName, string param)
        {
            _logger.LogTrace($"{methodName}({param})is worked out");
        }
        private void Logging(string methodName, string param1,string param2)
        {
            _logger.LogTrace($"{methodName}({param1}, {param2}) is worked out");
        }
        

        //xml if ti is public
        // or comments everywhere


        //Servers взаимодействие с клиентомы
        //взаимодейстиве с бд
        //useServer patterns
        //Защита по уровням
        //Защита по функциональности
        //обзаор программ связанный с доставкой еды 
        //выюор стека технологии (обзор)
        // прикинуть нагрузку на бд и вырать бд например или какой либо сервис

        // method Assert(strExp)
        // method Log()

        public ClientsInMsSqlRepository(InstantEatDbContext db, ILogger<ClientsInMsSqlRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            await Task.CompletedTask;
            Logging(nameof(GetAllClients));

            return _db.Clients;
        }


        public async Task<Client> GetClient(Guid id)
        {
            Logging(nameof(GetClient), nameof(id));

            if (id == Guid.Empty)
                throw new NullReferenceException($"{nameof(id)} is empty");

            return await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<Client> CreateClient(ClientCreateDto clientCreateDto)
        {
            Logging(nameof(CreateClient), nameof(clientCreateDto));

            if (clientCreateDto == null)
                throw new NullReferenceException($"{nameof(clientCreateDto)} param is null");

            var newClient = clientCreateDto.ToEntity();

            await _db.Clients.AddAsync(newClient);
            await _db.SaveChangesAsync();

            return newClient;
        }


        public async Task<bool> UpdateClient(Guid id, ClientCreateDto clientCreateDto)
        {
            Logging(nameof(UpdateClient), nameof(id), nameof(clientCreateDto));

            if (clientCreateDto == null)
                throw new NullReferenceException($"{nameof(clientCreateDto)} param is null");

            if (id == Guid.Empty)
                throw new NullReferenceException($"{nameof(id)} is empty");

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

            if (phoneNumber == null)
                throw new NullReferenceException($"{nameof(phoneNumber)} param is null");

            if (name == null)
                throw new NullReferenceException($"{nameof(name)} param is null");

            var client = await GetClientByPhoneNumber(phoneNumber);

            if (client == null) return false;

            client.Name = name;

            _db.Clients.Update(client);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteClient(Guid id)
        {
            Logging(nameof(DeleteClient), nameof(id));

            if (id == Guid.Empty)
                throw new NullReferenceException($"{nameof(id)}  param is empty");

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
