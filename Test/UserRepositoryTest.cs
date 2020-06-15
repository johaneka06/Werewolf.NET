using System;
using Xunit;
using Npgsql;
using NpgsqlTypes;
using Werewolf.NET.Game.Database.PostgreSQL;
using Werewolf.NET.Game;

namespace Test
{
    public class UserRepositoryTest
    {
        private string connectionStr;

        public UserRepositoryTest()
        {
            connectionStr = "Host=localhost;Username=postgres;Password=postgres;Database=WerewolfDB;Port=5432";
        }

        [Fact]
        public void CreateUser()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionStr);
            _connection.Open();

            IUserRepository userRepo = new UserRepository(_connection, null);

            User u = User.createUser("Angela");
            userRepo.Create(u);

            User userFound = userRepo.FindById(u.ID);
            Assert.NotNull(userFound);

            Assert.Equal(u.ID, userFound.ID);
            Assert.Equal(u.Name, userFound.Name);
            Assert.Equal(0, userFound.XP);
            Assert.Equal(0, userFound.XP);

            _connection.Close();
        }

        [Fact]
        public void AddExp()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionStr);
            _connection.Open();

            IUserRepository userRepo = new UserRepository(_connection, null);

            User u = User.createUser("Angela");
            userRepo.Create(u);

            userRepo.AddExp(u, 5);
            userRepo.AddExp(u, 10);
            userRepo.AddExp(u, 15);

            User userFound = userRepo.FindById(u.ID);
            Assert.True(userRepo != null);

            Assert.Equal(30, userFound.XP);

            _connection.Close();
        }

        [Fact]
        public void SnapshotTest()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionStr);
            _connection.Open();

            IUserRepository repo = new UserRepository(_connection, null);

            User u = User.createUser("Alicia");
            repo.Create(u);

            for (int i = 1; i <= 75; i++)
            {
                repo.AddExp(u, 3);
            }

            User u2 = repo.FindById(u.ID);
            Assert.Equal(225, u2.XP);

            int expFromSnapshot = 0;
            string query = "SELECT expvalue FROM exp_snapshot WHERE userid = @id ORDER BY created_at DESC LIMIT 1";
            using (var cmd = new NpgsqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("id", u.ID);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        expFromSnapshot = reader.GetInt32(0);
                    }
                }
            }

            Assert.Equal(150, expFromSnapshot);

            _connection.Close();
        }
    }
}