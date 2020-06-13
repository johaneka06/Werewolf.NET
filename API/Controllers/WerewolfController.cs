using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Werewolf.NET.Game;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WerewolfController : ControllerBase
    {
        private readonly ILogger<WerewolfController> _logger;

        public WerewolfController(ILogger<WerewolfController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<newList> Get()
        {
            newList newlist = new newList();


            return Enumerable.Range(1, 1).Select(index => newlist).ToArray();
        }
    }

    public class newList
    {
        private List<User> Players;
        private List<int> PlayerRole;
        WolfNet game;

        public WolfNet Player
        {
            get
            {
                return this.game;
            }
        }

        public newList()
        {
            this.Players = new List<User>();
            this.PlayerRole = new List<int>();

            Players.Add(User.createUser("Ayu"));
            Players.Add(User.createUser("Bani"));
            Players.Add(User.createUser("Cinta"));
            Players.Add(User.createUser("Dita"));
            Players.Add(User.createUser("Ester"));
            Players.Add(User.createUser("Fina"));
            Players.Add(User.createUser("Grace"));
            Players.Add(User.createUser("Hanako"));
            Players.Add(User.createUser("Ivanka"));
            Players.Add(User.createUser("Jean"));

            PlayerRole.Add(1);
            PlayerRole.Add(3);
            PlayerRole.Add(2);
            PlayerRole.Add(2);
            PlayerRole.Add(2);
            PlayerRole.Add(1);
            PlayerRole.Add(2);
            PlayerRole.Add(2);
            PlayerRole.Add(1);
            PlayerRole.Add(2);

            init();
        }

        public void init(){
            IExpGainer win = new WerewolfWin();
            IExpGainer lose = new WerewolfLose();
            game = new WerewolfGame(win, lose, Players, PlayerRole);
        }

    }
}