using InstantEatService.Dto;
using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public interface IClientsRepository
    {
        Task<Client> CreateClient(ClientCreateDto clientCreateDto);
        Task<bool> DeleteClient(Guid id);
        void Dispose();
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClient(Guid id);
        Task<Client> GetClientByPhoneNumber(string number);
        Task<bool> UpdateClient(Guid id, ClientCreateDto clientCreateDto);
        Task<bool> UpdateClientName(string phoneNumber, string name);
    }
}