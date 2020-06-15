using System;
using Xunit;

using Npgsql;
using Werewolf.NET.Game;
using Werewolf.NET.Game.Database.PostgreSQL;

namespace Test
{
    public class RoomRepositoryTest
    {
        private string conectionStr;

        public RoomRepositoryTest()
        {
            conectionStr = "Host=localhost;Username=postgres;Password=postgres;Database=WerewolfDB;Port=5432";
        }

        [Fact]
        public void CreateRoom()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(conectionStr);
            _connection.Open();

            IRoomRepository roomrepo = new RoomRepository(_connection, null);

            Room r = new Room(6);
            roomrepo.Create(r);

            Room r2 = roomrepo.FindRoom(r.ID);
            Assert.True(r2 != null);

            _connection.Close();
        }

        [Fact]
        public void UpdateMaxPlayer()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(conectionStr);
            _connection.Open();

            IRoomRepository roomrepo = new RoomRepository(_connection, null);
            Room r = new Room(6);

            roomrepo.Create(r);
            roomrepo.UpdateMax(r, 10);

            Room r2 = roomrepo.FindRoom(r.ID);
            Assert.Equal(10, r2.MaxUser);
        }

        [Fact]
        public void CloseTest()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(conectionStr);
            _connection.Open();

            IRoomRepository roomrepo = new RoomRepository(_connection, null);
            Room r = new Room(6);

            roomrepo.Create(r);

            roomrepo.Close(r);
            Assert.True(roomrepo.FindRoom(r.ID) == null);
        }
    }
}