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
            DotNetEnv.Env.Load(); 
            postgreUnitOfWork unit = new postgreUnitOfWork(System.Environment.GetEnvironmentVariable("CONN_STR"));
            
            List<User> users = unit.UserRepo.GetAllUser();

            return users;
        }

    }
}