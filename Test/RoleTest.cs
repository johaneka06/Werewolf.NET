using System;
using Xunit;

using Werewolf.NET.Game;

namespace Test
{
    public class RoleTest
    {
        Roles werewolf;
        Roles villager;
        Roles seer;

        public RoleTest(){
            werewolf = Roles.createRole(1, "Werewolf");
            villager = Roles.createRole(2, "Villager");
            seer = Roles.createRole(3, "Seer");
        }

        [Fact]
        public void CreateRole()
        {
            Roles r = Roles.createRole(1, "Werewolf");
            Assert.Equal(1, r.ID);
            Assert.Equal("Werewolf", r.RoleName);
        }

        [Fact]
        public void AddPlayer()
        {
            werewolf.AddPlayer(User.createUser("Budi"));
            Assert.Equal("Budi", werewolf.Player[0].Name);

            villager.AddPlayer(User.createUser("Amir"));
            villager.AddPlayer(User.createUser("Caca"));
            villager.AddPlayer(User.createUser("Dani"));
            Assert.Equal(3, villager.Player.Count);

            seer.AddPlayer(User.createUser("Martha"));
            Assert.Equal("Martha", seer.Player[0].Name);
        }

        [Fact]
        public void RemovePlayer()
        {
            User us = User.createUser("Dani");

            villager.AddPlayer(User.createUser("Amir"));
            villager.AddPlayer(User.createUser("Caca"));
            villager.AddPlayer(us);

            villager.removePlayer(us);
            Assert.Equal(2, villager.Player.Count);
        }
    }
}