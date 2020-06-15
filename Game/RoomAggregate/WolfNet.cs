using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{
    public abstract class WolfNet : IObservable<WerewolfResult>
    {
        protected List<Guid> _players;
        protected List<int> _userRoles;
        protected Roles werewolf;
        protected Roles villager;
        protected Roles seer;
        protected bool isNight;
        protected int count;
        private Guid _id;

        public Guid ID
        {
            get
            {
                return this._id;
            }
        }

        public Roles Werewolf
        {
            get
            {
                return this.werewolf;
            }
        }

        public Roles Villager
        {
            get
            {
                return this.villager;
            }
        }

        public Roles Seer
        {
            get
            {
                return this.seer;
            }
        }

        public WolfNet()
        {
            this._id = Guid.NewGuid();
        }

        public abstract void Execute(Vote vote);
        public abstract void Vote(Vote vote);
        protected abstract void ExecuteVillager(Guid killed);
        protected abstract bool ExecuteWolf(Guid killed);
        public abstract string GetGameName();
        public abstract object GetMemento();
        public abstract void LoadMemento(object memento);

        protected List<IObserver<WerewolfResult>> _observer = new List<IObserver<WerewolfResult>>();
        public void Attach(IObserver<WerewolfResult> obs)
        {
            _observer.Add(obs);
        }
        public void Broadcast(WerewolfResult evnt)
        {
            foreach (var obs in _observer)
            {
                obs.Update(evnt);
            }
        }
    }

    public abstract class Vote
    {
        protected Guid currentPlayer;

        public Guid Current
        {
            get
            {
                return this.currentPlayer;
            }
        }

        public Vote(Guid current)
        {
            this.currentPlayer = current;
        }

        public Vote()
        {
            this.currentPlayer = new Guid();
        }

    }

}