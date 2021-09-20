using InstantEatService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace InstantEatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        InstantEatDbContext _db;
        public TestController(InstantEatDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public string Get()
        { 
            var clients = _db.Clients.ToList<Client>();
            var jsonData = JsonSerializer.Serialize(clients);
            return jsonData;
        }

       
    }
}
