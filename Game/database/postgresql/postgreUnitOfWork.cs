using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Npgsql;
using NpgsqlTypes;

namespace Werewolf.NET.Game.Database.PostgreSQL
{
    public class postgreUnitOfWork : UnitOfWork
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IRoomRepository _roomRepository;

        public postgreUnitOfWork(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            if (_transaction != null) Console.WriteLine("connection success");
        }

        public IUserRepository UserRepo
        {
            get
            {
                if (_userRepository == null) _userRepository = new UserRepository(_connection, _transaction);
                return _userRepository;
            }
        }

        public IRoleRepository RoleRepo
        {
            get
            {
                if (_roleRepository == null) _roleRepository = new RoleRepository(_connection, _transaction);
                return _roleRepository;
            }
        }

        public IRoomRepository RoomRepo
        {
            get
            {
                if (_roomRepository == null) _roomRepository = new RoomRepository(_connection, _transaction);
                return _roomRepository;
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _connection.Close();
                }

                disposed = true;
            }
        }

    }
}