using System;
using System.Collections.Generic;
using Xunit;
using Npgsql;

using Werewolf.NET.Game;
using Werewolf.NET.Game.Database.PostgreSQL;

namespace Test
{
    public class RoleRepositoryTest
    {
        private string connectionStr;

        public RoleRepositoryTest(){
            connectionStr = "Host=localhost;Username=postgres;Password=postgres;Database=WerewolfDB;Port=5432";
        }

        [Fact]
        public void CreateRoleAndRead()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionStr);
            _connection.Open();

            IRoleRepository roleRepo = new RoleRepository(_connection, null);
            Roles werewolf = Roles.createRole(1, "Werewolf");
            Roles villager = Roles.createRole(2, "Villager");
            Roles seer = Roles.createRole(3, "seer");
            roleRepo.Create(werewolf);
            roleRepo.Create(villager);
            roleRepo.Create(seer);

            List<DBRole> found = roleRepo.GetAllRoles();
            Assert.NotEmpty(found);

            _connection.Close();
        }
    }
}