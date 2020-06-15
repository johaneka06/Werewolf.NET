using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace Werewolf.NET.Game.Database.PostgreSQL
{
    public class UserRepository : IUserRepository
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public UserRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public User FindById(Guid id)
        {
            string query =
                "SELECT username from \"user\" WHERE userId = @id";

            string name = "";

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name = reader.GetString(0);
                    }
                }
            }
            User u = new User(id, name, new Exp(GetExp(id)));
            return u;
        }

        public List<User> GetAllUser()
        {
            string query = @"SELECT u.userId, username, coalesce(xp, 0) FROM ""user"" u
                LEFT JOIN
                    (SELECT userId, sum(xpvalue) as xp FROM exp GROUP BY userId) e ON e.userId = u.userId";

            List<User> users = new List<User>();
            User user;

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User(reader.GetGuid(0), reader.GetString(1), new Exp(reader.GetInt32(2)));
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public void Create(User user)
        {
            string query = "INSERT INTO \"user\" (userid, username) VALUES(@id, @name)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", user.ID);
                cmd.Parameters.AddWithValue("name", user.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddExp(User user, int Exp)
        {
            string query = "INSERT INTO exp (expid, userid, xpvalue) VALUES(@xpId, @userid, @xpvalue)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("xpId", Guid.NewGuid());
                cmd.Parameters.AddWithValue("userid", user.ID);
                cmd.Parameters.AddWithValue("xpvalue", Exp);
                cmd.ExecuteNonQuery();
            }

            query = "SELECT COUNT(1) FROM exp WHERE userId = @id";
            int count = 0;
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", user.ID);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
            }

            if (count % 50 == 0) createSnapshot(user.ID);
        }

        private int GetExp(Guid id)
        {
            string query = "SELECT expvalue, lastSnapshotAt FROM exp_snapshot WHERE userid = @id";
            NpgsqlDateTime lastCreation = new NpgsqlDateTime(0);

            int sum = 0;

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sum = reader.GetInt32(0);
                        lastCreation = reader.GetTimeStamp(1);
                    }
                }
            }

            query = "SELECT coalesce(SUM(xpvalue), 0) FROM exp WHERE userId = @id AND created_at > @last";

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("last", lastCreation);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int currentExp = reader.GetInt32(0);
                        sum += currentExp;
                    }
                }
            }

            return sum;
        }

        private void createSnapshot(Guid id)
        {
            string query = "SELECT expid, created_at FROM exp WHERE userId = @id ORDER BY created_at DESC LIMIT 1";
            Guid lastExpId;
            NpgsqlDateTime lastCreation;

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lastExpId = reader.GetGuid(0);
                        lastCreation = reader.GetTimeStamp(1);
                    }
                    else throw new Exception("Last exp not found");
                }
            }

            int sum = GetExp(id);

            query = "INSERT INTO exp_snapshot (snapshotId, lastSnapshotId, userId, expvalue, lastSnapshotAt) VALUES (@snapId, @lastSnap, @userId, @value, @lastDate)";

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("snapId", Guid.NewGuid());
                cmd.Parameters.AddWithValue("lastSnap", lastExpId);
                cmd.Parameters.AddWithValue("userId", id);
                cmd.Parameters.AddWithValue("value", sum);
                cmd.Parameters.AddWithValue("lastDate", lastCreation);
                cmd.ExecuteNonQuery();
            }
        }
    }
}