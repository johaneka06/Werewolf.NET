using System;

namespace Werewolf.NET.Game
{
    public interface UnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}