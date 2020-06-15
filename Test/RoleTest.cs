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
            User budi = User.createUser("Budi");
            werewolf.AddPlayer(budi.ID);
            Assert.Equal(budi.ID, werewolf.Player[0]);

            villager.AddPlayer(User.createUser("Amir").ID);
            villager.AddPlayer(User.createUser("Caca").ID);
            villager.AddPlayer(User.createUser("Dani").ID);
            Assert.Equal(3, villager.Player.Count);

            User martha = User.createUser("Martha");
            seer.AddPlayer(martha.ID);

            Assert.Equal(martha.ID, seer.Player[0]);
        }

        [Fact]
        public void RemovePlayer()
        {
            User us = User.createUser("Dani");

            villager.AddPlayer(User.createUser("Amir").ID);
            villager.AddPlayer(User.createUser("Caca").ID);
            villager.AddPlayer(us.ID);

            villager.removePlayer(us.ID);
            Assert.Equal(2, villager.Player.Count);
        }
    }
}