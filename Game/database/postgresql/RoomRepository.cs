using System;
using Npgsql;
using NpgsqlTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Werewolf.NET.Game.Database.PostgreSQL
{
    public class RoomRepository : IRoomRepository
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public RoomRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public void Create(Room room)
        {
            string query = "INSERT INTO \"game_room\" (roomid, maxplayer) VALUES (@id, @max)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", room.ID);
                cmd.Parameters.AddWithValue("max", room.MaxUser);
                cmd.ExecuteNonQuery();
            }
        }

        public void Join(Room room, User player, int RoleId)
        {
            string query = "INSERT INTO \"playing_room\" (roomid, userid, roleid) VALUES (@id, @playerId, @roleId)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", room.ID);
                cmd.Parameters.AddWithValue("playerId", player.ID);
                cmd.Parameters.AddWithValue("roleId", RoleId);
                cmd.ExecuteNonQuery();
            }
        }

        public Room FindRoom(Guid Id)
        {
            Room r;
            string query = @"SELECT roomId, maxplayer FROM ""game_room"" WHERE roomId = @id AND deleted_at is null";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", Id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int max = reader.GetInt32(1);
                        r = new Room(Id, max);
                        return r;
                    }
                }

            }

            return null;
        }

        public void UpdateMax(Room room, int max)
        {
            string query = "UPDATE \"game_room\" SET maxplayer = @max, updated_at = CURRENT_TIMESTAMP WHERE roomid = @id AND deleted_at is null";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("max", max);
                cmd.Parameters.AddWithValue("id", room.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Close(Room room)
        {
            string query = "UPDATE \"game_room\" SET deleted_at = CURRENT_TIMESTAMP WHERE roomid = @id AND deleted_at is null";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", room.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddVote(Room room, Vote vote)
        {
            string query = "INSERT INTO game_vote (voteId, roomId, vote, state) VALUES (@id, @roomid, @vote, @state)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("roomid", room.ID);

                string jsonStr = JsonSerializer.Serialize(room.Game.GetMemento());
                cmd.Parameters.Add(new NpgsqlParameter("vote", NpgsqlDbType.Jsonb) { Value = vote });
                cmd.Parameters.AddWithValue(new NpgsqlParameter("state", NpgsqlDbType.Jsonb) { Value = jsonStr });

                cmd.ExecuteNonQuery();
            }
        }
    }
}