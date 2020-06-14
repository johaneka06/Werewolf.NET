using System;
using System.Collections.Generic;
using Xunit;

using Werewolf.NET.Game;

namespace Test
{
    public class WerewolfTest
    {
        private List<User> Players;
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

            game = GameFactory.Create("werewolf", Players, PlayerRole);
        }

        [Fact]
        public void CheckGameName()
        {
            Assert.Equal("Werewolf", game.GetGameName());
        }

        [Fact]
        public void InvalidPlayer()
        {
            List<User> players = new List<User>();

            players.Add(User.createUser("Amir"));
            players.Add(User.createUser("Budi"));
            players.Add(User.createUser("Carlie"));
            players.Add(User.createUser("Darlie"));

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
        public void CheckHighestCount()
        {
            game.Vote(new WerewolfVote(Players[0], Players[1]));
            game.Vote(new WerewolfVote(Players[1], Players[0]));
            game.Vote(new WerewolfVote(Players[2], Players[0]));
            game.Vote(new WerewolfVote(Players[3], Players[0]));
            game.Vote(new WerewolfVote(Players[4], Players[1]));
            game.Vote(new WerewolfVote(Players[5], Players[1]));
            game.Vote(new WerewolfVote(Players[6], Players[1]));
            game.Vote(new WerewolfVote(Players[7], Players[2]));
            game.Vote(new WerewolfVote(Players[8], Players[1]));
            game.Vote(new WerewolfVote(Players[9], Players[7]));

            Assert.Equal(4, bani.XP);
        }

        [Fact]
        public void CheckLose()
        {

            game.Execute(new WerewolfVote(ayu, bani));
            game.Execute(new WerewolfVote(fina, bani));
            game.Execute(new WerewolfVote(ivanka, bani));

            game.Vote(new WerewolfVote(ayu, cinta));
            game.Vote(new WerewolfVote(cinta, ayu));
            game.Vote(new WerewolfVote(dita, cinta));
            game.Vote(new WerewolfVote(ester, cinta));
            game.Vote(new WerewolfVote(fina, cinta));
            game.Vote(new WerewolfVote(grace, cinta));
            game.Vote(new WerewolfVote(hanako, cinta));
            game.Vote(new WerewolfVote(ivanka, cinta));
            game.Vote(new WerewolfVote(jean, cinta));
            
            game.Execute(new WerewolfVote(ayu, dita));
            game.Execute(new WerewolfVote(fina, dita));
            game.Execute(new WerewolfVote(ivanka, dita));

            game.Vote(new WerewolfVote(ayu, ester));
            game.Vote(new WerewolfVote(ester, ayu));
            game.Vote(new WerewolfVote(fina, ester));
            game.Vote(new WerewolfVote(grace, ester));
            game.Vote(new WerewolfVote(hanako, ester));
            game.Vote(new WerewolfVote(ivanka, ester));
            game.Vote(new WerewolfVote(jean, ester));
            
            game.Execute(new WerewolfVote(ayu, grace));
            game.Execute(new WerewolfVote(fina, grace));
            game.Execute(new WerewolfVote(ivanka, grace));

            game.Vote(new WerewolfVote(ayu, hanako));
            game.Vote(new WerewolfVote(fina, hanako));
            game.Vote(new WerewolfVote(hanako, fina));
            game.Vote(new WerewolfVote(ivanka, hanako));
            game.Vote(new WerewolfVote(jean, hanako));

            game.Execute(new WerewolfVote(ayu, jean));
            game.Execute(new WerewolfVote(fina, jean));
            game.Execute(new WerewolfVote(ivanka, jean));

            Assert.Equal(4, bani.XP);
            Assert.Equal(8, ayu.XP);
            Assert.Equal(8, fina.XP);
        }

        [Fact]
        public void CheckWin()
        {
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

            Assert.Equal(4, ayu.XP);
            Assert.Equal(8, cinta.XP);
        }

    }
}