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
        public newList Get()
        {
            newList newlist = new newList();


            return newlist;
        }
    }

    public class newList
    {
        private List<Guid> Players;
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

        public WolfNet Game
        {
            get
            {
                return this.game;
            }
        }

        public newList()
        {
            this.Players = new List<Guid>();
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

            Players.Add(ayu.ID);
            Players.Add(bani.ID);
            Players.Add(cinta.ID);
            Players.Add(dita.ID);
            Players.Add(ester.ID);
            Players.Add(fina.ID);
            Players.Add(grace.ID);
            Players.Add(hanako.ID);
            Players.Add(ivanka.ID);
            Players.Add(jean.ID);

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

            game = GameFactory.Create("werewolf", Players, PlayerRole, null, "");

            CheckWin();
        }

        public void CheckWin()
        {
            game.Execute(new WerewolfVote(ayu.ID, bani.ID));
            game.Execute(new WerewolfVote(fina.ID, bani.ID));
            game.Execute(new WerewolfVote(ivanka.ID, bani.ID));

            game.Vote(new WerewolfVote(ayu.ID, cinta.ID));
            game.Vote(new WerewolfVote(cinta.ID, ayu.ID));
            game.Vote(new WerewolfVote(dita.ID, ayu.ID));
            game.Vote(new WerewolfVote(ester.ID, ayu.ID));
            game.Vote(new WerewolfVote(fina.ID, ayu.ID));
            game.Vote(new WerewolfVote(grace.ID, ayu.ID));
            game.Vote(new WerewolfVote(hanako.ID, ayu.ID));
            game.Vote(new WerewolfVote(ivanka.ID, ayu.ID));
            game.Vote(new WerewolfVote(jean.ID, ayu.ID));
            
            game.Execute(new WerewolfVote(fina.ID, dita.ID));
            game.Execute(new WerewolfVote(ivanka.ID, dita.ID));

            game.Vote(new WerewolfVote(cinta.ID, fina.ID));
            game.Vote(new WerewolfVote(ester.ID, fina.ID));
            game.Vote(new WerewolfVote(fina.ID, ester.ID));
            game.Vote(new WerewolfVote(grace.ID, fina.ID));
            game.Vote(new WerewolfVote(hanako.ID, fina.ID));
            game.Vote(new WerewolfVote(ivanka.ID, fina.ID));
            game.Vote(new WerewolfVote(jean.ID, fina.ID));
            
            game.Execute(new WerewolfVote(ivanka.ID, grace.ID));

            game.Vote(new WerewolfVote(cinta.ID, ivanka.ID));
            game.Vote(new WerewolfVote(ester.ID, ivanka.ID));
            game.Vote(new WerewolfVote(hanako.ID, ivanka.ID));
            game.Vote(new WerewolfVote(ivanka.ID, hanako.ID));
            game.Vote(new WerewolfVote(jean.ID, ivanka.ID));
        }

    }
}