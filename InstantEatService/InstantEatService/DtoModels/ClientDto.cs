using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.DtoModels
{
    public class ClientDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Role? Role { get; set; }

        public ClientDto(Client client)
        {
            Name = client.Name;
            PhoneNumber = client.PhoneNumber;
            Role = client.Role;
        }

    }
}
