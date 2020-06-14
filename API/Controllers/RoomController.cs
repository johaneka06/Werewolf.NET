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
    public class RoomController : ControllerBase
    {
        private readonly ILogger<WerewolfController> _logger;

        public RoomController(ILogger<WerewolfController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Roomy Get()
        {
            Roomy newlist = new Roomy();


            return newlist;
        }
    }

    public class Roomy
    {
        User ayu, bani, cinta, dita, ester, fina, grace, hanako, ivanka, jean;
        Room room;

        public Room Game
        {
            get
            {
                return this.room;
            }
        }

        public Roomy()
        {
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

            room = new Room(10);

            room.Join(ayu, 1);
            room.Join(bani, 3);
            room.Join(cinta, 2);
            room.Join(dita, 2);
            room.Join(ester, 2);
            room.Join(fina, 1);
            room.Join(grace, 2);
            room.Join(hanako, 2);
            room.Join(ivanka, 1);
            room.Join(jean, 2);

            room.StartGame("werewolf");

            CheckWin();
        }

        public void CheckWin()
        {
            room.Execute(new WerewolfVote(ayu, bani));
            room.Execute(new WerewolfVote(fina, bani));
            room.Execute(new WerewolfVote(ivanka, bani));

            room.Vote(new WerewolfVote(ayu, cinta));
            room.Vote(new WerewolfVote(cinta, ayu));
            room.Vote(new WerewolfVote(dita, ayu));
            room.Vote(new WerewolfVote(ester, ayu));
            room.Vote(new WerewolfVote(fina, ayu));
            room.Vote(new WerewolfVote(grace, ayu));
            room.Vote(new WerewolfVote(hanako, ayu));
            room.Vote(new WerewolfVote(ivanka, ayu));
            room.Vote(new WerewolfVote(jean, ayu));

            room.Execute(new WerewolfVote(fina, dita));
            room.Execute(new WerewolfVote(ivanka, dita));

            room.Vote(new WerewolfVote(cinta, fina));
            room.Vote(new WerewolfVote(ester, fina));
            room.Vote(new WerewolfVote(fina, ester));
            room.Vote(new WerewolfVote(grace, fina));
            room.Vote(new WerewolfVote(hanako, fina));
            room.Vote(new WerewolfVote(ivanka, fina));
            room.Vote(new WerewolfVote(jean, fina));

            room.Execute(new WerewolfVote(ivanka, grace));

            room.Vote(new WerewolfVote(cinta, ivanka));
            room.Vote(new WerewolfVote(ester, ivanka));
            room.Vote(new WerewolfVote(hanako, ivanka));
            room.Vote(new WerewolfVote(ivanka, hanako));
            room.Vote(new WerewolfVote(jean, ivanka));
        }

    }
}