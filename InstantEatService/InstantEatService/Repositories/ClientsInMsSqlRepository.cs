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
    public class ClientsInMsSqlRepository : IDisposable, IClientsRepository
    {
        private readonly InstantEatDbContext _db;
        private readonly ILogger<ClientsInMsSqlRepository> _logger;


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
            var function = nameof(GetAllClients);
            _logger.LogTrace($"{function}() is worked out");

            return _db.Clients;
        }


        public async Task<Client> GetClient(Guid id)
        {
            var function = nameof(GetClient);
            _logger.LogTrace($"{function}({nameof(id)}) is worked out");

            if (id == Guid.Empty)
                throw new NullReferenceException($"{nameof(id)} is empty");

            return await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

       
        public async Task<Client> CreateClient(ClientCreateDto clientCreateDto)
        {
            var function = nameof(CreateClient);
            _logger.LogTrace($"{function}({nameof(clientCreateDto)}) is worked out");

            if (clientCreateDto == null)
                throw new NullReferenceException($"{nameof(clientCreateDto)} param is null");

            var newClient = clientCreateDto.ToEntity();

            await _db.Clients.AddAsync(newClient);
            await _db.SaveChangesAsync();

            return newClient;
        }


        public async Task<bool> UpdateClient(Guid id, ClientCreateDto clientCreateDto)
        {
            var function = nameof(UpdateClient);
            _logger.LogTrace($"{function}({nameof(id)}, {nameof(clientCreateDto)}) is worked out");

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

        public async Task<bool> DeleteClient(Guid id)
        {
            var function = nameof(DeleteClient);
            _logger.LogTrace($"{function}({nameof(id)}) is worked out");

            if (id == Guid.Empty)
                throw new NullReferenceException($"{nameof(id)}  param is empty") ;

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
