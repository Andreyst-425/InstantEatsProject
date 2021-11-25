using InstantEatService.Models;

namespace InstantEatService.Dto
{
    public class ClientCreateDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        public Client ToEntity()
        {
            return new Client()
            {
                Name = Name,
                PhoneNumber = PhoneNumber,
            };
        }
    }
}
