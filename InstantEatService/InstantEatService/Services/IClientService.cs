using InstantEatService.Dto;
using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface IClientService
    {
        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteClient(int id);

        /// <summary>
        /// Получить всех клиентов
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
        /// Изменить информацию о клиенте
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> PatchClientName(string phoneNumber, string name);

        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<bool> PostClient(string phoneNumber);

        /// <summary>
        /// Обновить данные о клиенте
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientCreateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateClient(int id, ClientCreateDto clientCreateDto);

        /// <summary>
        /// Изменить имя клиента
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> UpdateClientName(string phoneNumber, string name);
    }
}