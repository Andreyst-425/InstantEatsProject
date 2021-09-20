using InstantEatService.DtoModels;
using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public interface IClients
    {
        Task<Client> CreateClient(ClientCreateDto clientCreateDto);
        Task<bool> DeleteClient(Guid id);
        void Dispose();
        IEnumerable<Client> GetAllClients();
        Task<Client> GetClient(Guid id);
        Task<bool> UpdateClient(Guid id, string name, string phoneNumber);
    }
}