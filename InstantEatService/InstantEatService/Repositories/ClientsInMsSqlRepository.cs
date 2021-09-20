using InstantEatService.DtoModels;
using InstantEatService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public class ClientsInMsSqlRepository : IDisposable, IClients
    {
        private readonly InstantEatDbContext _db;
        private readonly ILogger<ClientsInMsSqlRepository> _logger;

        public ClientsInMsSqlRepository(InstantEatDbContext db, ILogger<ClientsInMsSqlRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<Client> GetAllClients()
        {
            var function = nameof(GetAllClients);
            _logger.LogTrace($"{function}() is worked out");

            return _db.Clients;
        }


        public async Task<Client> GetClient(Guid id)
        {
            var function = nameof(GetClient);
            _logger.LogTrace($"{function}() is worked out");

            if (id == Guid.Empty)
                throw new NullReferenceException("Client id is empty");

            return await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client> CreateClient(ClientCreateDto clientCreateDto)
        {
            var function = nameof(CreateClient);
            _logger.LogTrace($"{function}() is worked out");

            if (clientCreateDto == null)
                throw new NullReferenceException("clientCreateDto param is null");

            var newClient = clientCreateDto.ToEntity();

            await _db.Clients.AddAsync(newClient);
            await _db.SaveChangesAsync();

            return newClient;
        }

        public async Task<bool> UpdateClient(Guid id, string name, string phoneNumber)
        {
            var function = nameof(UpdateClient);
            _logger.LogTrace($"{function}() is worked out");

            //Разобраться с null и пустыми объектами

            if (name == null)
                throw new NullReferenceException($"{nameof(name)} param is null");

            if (phoneNumber == null)
                throw new NullReferenceException($"{nameof(phoneNumber)} param is null");

            if (id == Guid.Empty)
                throw new NullReferenceException("Client id is empty");

            var client = await GetClient(id);

            if (client == null) return false;

            client.Name = name;
            client.PhoneNumber = phoneNumber;

            _db.Update(client);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteClient(Guid id)
        {
            var function = nameof(DeleteClient);
            _logger.LogTrace($"{function}() is worked out");

            if (id == Guid.Empty)
                throw new NullReferenceException("Client id param is empty");

            var client = await GetClient(id);

            if (client == null) return false;

            _db.Remove(client);
            await _db.SaveChangesAsync();

            return true;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
