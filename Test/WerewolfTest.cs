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
        WolfNet game;
        public WerewolfTest()
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

            game = new WerewolfGame(Players, PlayerRole);

            game.Vote(new WerewolfVote(Players[0], Players[3], 1));
            game.Vote(new WerewolfVote(Players[5], Players[3], 2));
            try
            {
                game.Vote(new WerewolfVote(Players[7], Players[2], 3));
            }
            catch (Exception e)
            {
                res = e;
            }
            game.Vote(new WerewolfVote(Players[1], Players[4], 1));

            Assert.True(res != null);
        }

    }
}