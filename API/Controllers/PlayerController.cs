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
            newClass nw = new newClass();
            return nw.Player;
        }

    }
    public class newClass{
        private List<User> Players;
        private List<int> PlayerRole;
        WolfNet game;

        User ayu;
        User bani;
        User cinta;
        User dita;
        User ester;
        User fina;
        User grace;
        User hanako;
        User ivanka;
        User jean;

        public List<User> Player
        {
            get
            {
                return this.Players;
            }
        }

        public newClass()
        {
            this.Players = new List<User>();
            this.PlayerRole = new List<int>();

            ayu = User.createUser("Ayu");
            bani = User.createUser("Bani");
            cinta = User.createUser("Cinta");
            dita = User.createUser("Dita");
            ester = User.createUser("Ester");
            fina = User.createUser("Fina");
            grace = User.createUser("Grace");
            hanako = User.createUser("Hanako");
            ivanka = User.createUser("Ivanka");
            jean = User.createUser("Jean");

            Players.Add(ayu);
            Players.Add(bani);
            Players.Add(cinta);
            Players.Add(dita);
            Players.Add(ester);
            Players.Add(fina);
            Players.Add(grace);
            Players.Add(hanako);
            Players.Add(ivanka);
            Players.Add(jean);

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

            win = new WerewolfWin();
            lose = new WerewolfLose();

            CheckWin();
        }
        IExpGainer win;
        IExpGainer lose;

        public void init()
        {
            win = new WerewolfWin();
            lose = new WerewolfLose();
            game = new WerewolfGame(win, lose, Players, PlayerRole);
        }

        public void CheckWin()
        {
            game = new WerewolfGame(win, lose, Players, PlayerRole);

            game.Execute(new WerewolfVote(ayu, bani));
            game.Execute(new WerewolfVote(fina, bani));
            game.Execute(new WerewolfVote(ivanka, bani));

            game.Vote(new WerewolfVote(ayu, cinta));
            game.Vote(new WerewolfVote(cinta, ayu));
            game.Vote(new WerewolfVote(dita, ayu));
            game.Vote(new WerewolfVote(ester, ayu));
            game.Vote(new WerewolfVote(fina, ayu));
            game.Vote(new WerewolfVote(grace, ayu));
            game.Vote(new WerewolfVote(hanako, ayu));
            game.Vote(new WerewolfVote(ivanka, ayu));
            game.Vote(new WerewolfVote(jean, ayu));

            game.Execute(new WerewolfVote(fina, dita));
            game.Execute(new WerewolfVote(ivanka, dita));

            game.Vote(new WerewolfVote(cinta, fina));
            game.Vote(new WerewolfVote(ester, fina));
            game.Vote(new WerewolfVote(fina, ester));
            game.Vote(new WerewolfVote(grace, fina));
            game.Vote(new WerewolfVote(hanako, fina));
            game.Vote(new WerewolfVote(ivanka, fina));
            game.Vote(new WerewolfVote(jean, fina));

            game.Execute(new WerewolfVote(ivanka, grace));

            game.Vote(new WerewolfVote(cinta, ivanka));
            game.Vote(new WerewolfVote(ester, ivanka));
            game.Vote(new WerewolfVote(hanako, ivanka));
            game.Vote(new WerewolfVote(ivanka, hanako));
            game.Vote(new WerewolfVote(jean, ivanka));
        }

    }
}