using InstantEatService.Models;

namespace InstantEatService.Dto
{
    public class ClientDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        public ClientDto(Client client)
        {
            Name = client.Name;
            PhoneNumber = client.PhoneNumber;
        }
    }
}
