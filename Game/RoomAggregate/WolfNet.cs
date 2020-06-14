using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{
    public abstract class WolfNet : IObservable<WerewolfResult>
    {
        protected List<User> _players;
        protected List<int> _userRoles;
        protected Roles werewolf;
        protected Roles villager;
        protected Roles seer;
        protected bool isNight;
        protected int count;

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
        
        public abstract void Execute(Vote vote);
        public abstract void Vote(Vote vote);
        protected abstract void ExecuteVillager(User killed);
        protected abstract bool ExecuteWolf(User killed);
        public abstract string GetGameName();

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
        protected User currentPlayer;

        public User Current
        {
            get
            {
                return this.currentPlayer;
            }
        }

        public Vote(User current)
        {
            this.currentPlayer = current;
        }

        public Vote()
        {
            this.currentPlayer = new User();
        }

    }

}