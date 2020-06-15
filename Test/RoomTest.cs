using System;
using System.Collections.Generic;
using Xunit;
using Werewolf.NET.Game;

namespace Test
{
    public class RoomTest
    {
        User ayu, bani, cinta, dita, ester, fina, grace, hanako, ivanka, jean;
        Room room;

        public RoomTest()
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
        }

        [Fact]
        public void MaxUserCheck()
        {
            Exception ex = null;
            try
            {
                room.Join(User.createUser("Rogueuser"), 2);
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.True(ex != null);
        }

        [Fact]
        public void CheckWin()
        {
            room.Execute(new WerewolfVote(ayu.ID, bani.ID));
            room.Execute(new WerewolfVote(fina.ID, bani.ID));
            room.Execute(new WerewolfVote(ivanka.ID, bani.ID));

            room.Vote(new WerewolfVote(ayu.ID, cinta.ID));
            room.Vote(new WerewolfVote(cinta.ID, ayu.ID));
            room.Vote(new WerewolfVote(dita.ID, ayu.ID));
            room.Vote(new WerewolfVote(ester.ID, ayu.ID));
            room.Vote(new WerewolfVote(fina.ID, ayu.ID));
            room.Vote(new WerewolfVote(grace.ID, ayu.ID));
            room.Vote(new WerewolfVote(hanako.ID, ayu.ID));
            room.Vote(new WerewolfVote(ivanka.ID, ayu.ID));
            room.Vote(new WerewolfVote(jean.ID, ayu.ID));

            room.Execute(new WerewolfVote(fina.ID, dita.ID));
            room.Execute(new WerewolfVote(ivanka.ID, dita.ID));

            room.Vote(new WerewolfVote(cinta.ID, fina.ID));
            room.Vote(new WerewolfVote(ester.ID, fina.ID));
            room.Vote(new WerewolfVote(fina.ID, ester.ID));
            room.Vote(new WerewolfVote(grace.ID, fina.ID));
            room.Vote(new WerewolfVote(hanako.ID, fina.ID));
            room.Vote(new WerewolfVote(ivanka.ID, fina.ID));
            room.Vote(new WerewolfVote(jean.ID, fina.ID));

            room.Execute(new WerewolfVote(ivanka.ID, grace.ID));

            room.Vote(new WerewolfVote(cinta.ID, ivanka.ID));
            room.Vote(new WerewolfVote(ester.ID, ivanka.ID));
            room.Vote(new WerewolfVote(hanako.ID, ivanka.ID));
            room.Vote(new WerewolfVote(ivanka.ID, hanako.ID));
            room.Vote(new WerewolfVote(jean.ID, ivanka.ID));

            Exception ex = null;
            try
            {
                room.Vote(new WerewolfVote(cinta.ID, hanako.ID));
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.True(ex != null);
        }
    }
}