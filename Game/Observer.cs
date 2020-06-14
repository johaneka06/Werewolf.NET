using System;

namespace Werewolf.NET.Game
{
    public interface IObserver<T>
    {
        void Update(T e);
    }

    public interface IObservable<T>
    {
        void Attach(IObserver<T> obs);
        void Broadcast(T e);
    }
}