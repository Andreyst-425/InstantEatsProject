using InstantEatService.Dto;
using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public interface IClientsRepository
    {
        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="clientCreateDto"></param>
        /// <returns></returns>
        Task<Client> CreateClient(ClientCreateDto clientCreateDto);

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteClient(int id);
        
        /// <summary>
        /// Очистка ресурсов
        /// </summary>
        void Dispose();

        /// <summary>
        /// Получить список клиентов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Client>> GetAllClients();

        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Client> GetClient(int id);

        /// <summary>
        /// Получить клиента по номеру телефона
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        Task<Client> GetClientByPhoneNumber(string number);

        /// <summary>
        /// Изменить денные о клиенте по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientCreateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateClient(int id, ClientCreateDto clientCreateDto);

        /// <summary>
        /// Изменить имя клиента по номеру телефона
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> UpdateClientName(string phoneNumber, string name);
    }
}