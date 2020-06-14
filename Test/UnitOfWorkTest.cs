using System;
using Xunit;

using Npgsql;
using Werewolf.NET.Game.Database.PostgreSQL;

namespace Test
{
    public class UnitOfWorkTest
    {
        string connection;
        public UnitOfWorkTest()
        {
            connection = "Host=localhost;Username=postgres;Password=postgres;Database=WerewolfDB;Port=5432";
        }
        
    }
}