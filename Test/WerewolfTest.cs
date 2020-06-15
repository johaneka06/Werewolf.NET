using System;
using System.Collections.Generic;
using Xunit;

using Werewolf.NET.Game;

namespace Test
{
    public class WerewolfTest
    {
        private List<Guid> Players;
        private List<int> PlayerRole;
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
        WolfNet game;

        public WerewolfTest()
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
        }

        [Fact]
        public void CheckGameName()
        {
            Assert.Equal("Werewolf", game.GetGameName());
        }

        [Fact]
        public void InvalidPlayer()
        {
            List<Guid> players = new List<Guid>();

            players.Add(User.createUser("Amir").ID);
            players.Add(User.createUser("Budi").ID);
            players.Add(User.createUser("Carlie").ID);
            players.Add(User.createUser("Darlie").ID);

            List<int> UserRole = new List<int>();

            UserRole.Add(1);
            UserRole.Add(2);
            UserRole.Add(2);
            UserRole.Add(2);

            Exception res = null;

            try
            {
                WolfNet ww = new WerewolfGame(players, UserRole);
            }
            catch (Exception e)
            {
                res = e;
            }
            Assert.True(res != null);
        }

        [Fact]
        public void InvalidMove()
        {
            Exception res = null;

            game.Execute(new WerewolfVote(Players[0], Players[3]));
            game.Execute(new WerewolfVote(Players[5], Players[3]));
            try
            {
                game.Execute(new WerewolfVote(Players[7], Players[3]));
            }
            catch (Exception e)
            {
                res = e;
            }
            game.Execute(new WerewolfVote(Players[1], Players[4]));

            Assert.True(res != null);
        }

        [Fact]
        public void CheckLose()
        {

            game.Execute(new WerewolfVote(ayu.ID, bani.ID));
            game.Execute(new WerewolfVote(fina.ID, bani.ID));
            game.Execute(new WerewolfVote(ivanka.ID, bani.ID));

            game.Vote(new WerewolfVote(ayu.ID, cinta.ID));
            game.Vote(new WerewolfVote(cinta.ID, ayu.ID));
            game.Vote(new WerewolfVote(dita.ID, cinta.ID));
            game.Vote(new WerewolfVote(ester.ID, cinta.ID));
            game.Vote(new WerewolfVote(fina.ID, cinta.ID));
            game.Vote(new WerewolfVote(grace.ID, cinta.ID));
            game.Vote(new WerewolfVote(hanako.ID, cinta.ID));
            game.Vote(new WerewolfVote(ivanka.ID, cinta.ID));
            game.Vote(new WerewolfVote(jean.ID, cinta.ID));
            
            game.Execute(new WerewolfVote(ayu.ID, dita.ID));
            game.Execute(new WerewolfVote(fina.ID, dita.ID));
            game.Execute(new WerewolfVote(ivanka.ID, dita.ID));

            game.Vote(new WerewolfVote(ayu.ID, ester.ID));
            game.Vote(new WerewolfVote(ester.ID, ayu.ID));
            game.Vote(new WerewolfVote(fina.ID, ester.ID));
            game.Vote(new WerewolfVote(grace.ID, ester.ID));
            game.Vote(new WerewolfVote(hanako.ID, ester.ID));
            game.Vote(new WerewolfVote(ivanka.ID, ester.ID));
            game.Vote(new WerewolfVote(jean.ID, ester.ID));
            
            game.Execute(new WerewolfVote(ayu.ID, grace.ID));
            game.Execute(new WerewolfVote(fina.ID, grace.ID));
            game.Execute(new WerewolfVote(ivanka.ID, grace.ID));

            game.Vote(new WerewolfVote(ayu.ID, hanako.ID));
            game.Vote(new WerewolfVote(fina.ID, hanako.ID));
            game.Vote(new WerewolfVote(hanako.ID, fina.ID));
            game.Vote(new WerewolfVote(ivanka.ID, hanako.ID));
            game.Vote(new WerewolfVote(jean.ID, hanako.ID));

            game.Execute(new WerewolfVote(ayu.ID, jean.ID));
            game.Execute(new WerewolfVote(fina.ID, jean.ID));
            game.Execute(new WerewolfVote(ivanka.ID, jean.ID));

            Assert.True(game.Villager.Player.Count == 0);
            Assert.True(game.Werewolf.Player.Count > 0);
        }

        [Fact]
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

            Assert.True(game.Werewolf.Player.Count == 0);
            Assert.True(game.Villager.Player.Count > 0);
        }

    }
}