using InstantEatService.Dto;
using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public interface IClientsRepository
    {
        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="clientCreateDto"> Данные для создания клиента </param>
        /// <returns></returns>
        Task<Client> CreateClient(ClientCreateDto clientCreateDto);

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id"> Идентификатор клиента </param>
        /// <returns></returns>
        Task<bool> DeleteClient(int id);
        
        /// <summary>
        /// Очистка ресурсов
        /// </summary>
        void Dispose();

        /// <summary>
        /// Получить список всех клиентов
        /// </summary>
        /// <returns></returns>
        Task<List<Client>> GetAllClients();

        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id"> Идентификатор клиента </param>
        /// <returns></returns>
        Task<Client> GetClient(int id);

        /// <summary>
        /// Получить клиента по номеру телефона
        /// </summary>
        /// <param name="number"> Номер телефона </param>
        /// <returns></returns>
        Task<Client> GetClientByPhoneNumber(string number);

        /// <summary>
        /// Изменить данные о клиенте по id
        /// </summary>
        /// <param name="id"> Идентификатор клиента </param>
        /// <param name="clientCreateDto"> Новые данные клиента </param>
        /// <returns></returns>
        Task<bool> UpdateClient(int id, ClientCreateDto clientCreateDto);

        /// <summary>
        /// Изменить имя клиента по номеру телефона
        /// </summary>
        /// <param name="phoneNumber"> Номер телефона </param>
        /// <param name="name"> Имя </param>
        /// <returns></returns>
        Task<bool> UpdateClientName(string phoneNumber, string name);
    }
}