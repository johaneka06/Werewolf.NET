using System;
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
                @"SELECT username, coalesce(xp, 0) from ""user""
                LEFT JOIN
                (
                    SELECT userId, SUM(xpvalue) as xp
                    FROM exp
                    GROUP BY userId
                ) e ON ""user"".userId = e.userId
                WHERE ""user"".userId = @id";

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader.GetString(0);
                        int xpvalue = reader.GetInt32(1);

                        User u = new User(id, name, new Exp(xpvalue));
                        return u;
                    }
                }
            }
            return null;
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
        }
    }
}