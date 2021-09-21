using InstantEatService.DtoModels;
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
        /// Удалить клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteClient(Guid id);

        /// <summary>
        /// Очистка данных
        /// </summary>
        void Dispose();

        /// <summary>
        /// Получить список всех клиентов
        /// </summary>
        /// <returns></returns>
        IEnumerable<Client> GetAllClients();
        
        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Client> GetClient(Guid id);

        /// <summary>
        /// Обновить клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientCreateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateClient(Guid id, ClientCreateDto clientCreateDto);
    }
}