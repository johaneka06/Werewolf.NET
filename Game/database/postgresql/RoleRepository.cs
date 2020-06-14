using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace Werewolf.NET.Game.Database.PostgreSQL
{
    public class RoleRepository : IRoleRepository
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public RoleRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<DBRole> GetAllRoles()
        {
            string query = "SELECT roleId, roleName, created_at FROM \"role\"";

            List<DBRole> fetched = new List<DBRole>();

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DBRole _role = new DBRole(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
                        fetched.Add(_role);
                    }
                }
                return fetched;
            }
        }

        public void Create(Roles newRole)
        {
            string query = "INSERT INTO \"role\" (rolename) VALUES (@name)";

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("name", newRole.RoleName);
                cmd.ExecuteNonQuery();
            }
        }
    }
}