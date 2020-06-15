using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Werewolf.NET.Game;
using Werewolf.NET.Game.Database.PostgreSQL;
using Npgsql;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<User> Get()
        {
            string connectionStr = "Host=localhost;Username=postgres;Password=postgres;Database=WerewolfDB;Port=5432";
            NpgsqlConnection _connection = new NpgsqlConnection(connectionStr);
            _connection.Open();

            IUserRepository repo = new UserRepository(_connection, null);

            List<User> users = repo.GetAllUser();

            _connection.Close();

            return users;
        }

    }
}