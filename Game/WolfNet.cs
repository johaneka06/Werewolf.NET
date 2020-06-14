using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{
    public abstract class WolfNet : IObservable<WerewolfResult>
    {
        protected bool _gameEnded;

        protected List<User> _players;
        protected List<int> _userRoles;
        protected Roles werewolf;
        protected Roles villager;
        protected Roles seer;
        protected bool isNight;
        protected int specialPersonCount;
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

        protected int totalPlayer;
        public WolfNet(List<User> players, List<int> UserRoles)
        {
            this.werewolf = new Roles(1, new RoleName("Werewolf"));
            this.villager = new Roles(2, new RoleName("Villager"));
            this.seer = new Roles(3, new RoleName("Seer"));

            this._players = players;
            this._userRoles = UserRoles;
            this._gameEnded = false;
            this.isNight = true;
            this.count = 0;
            this.totalPlayer = _players.Count;

            this.Init();
        }

        public void Vote(Vote vote)
        {
            if (_gameEnded) throw new Exception("Game already ended");

            DoVote(vote);
        }

        public void Execute(Vote vote)
        {
            if (_gameEnded) throw new Exception("Game already ended");

            DoExecute(vote);
        }

        protected abstract void Init();
        protected abstract void DoExecute(Vote vote);
        protected abstract void DoVote(Vote vote);
        protected abstract void ExecuteVillager(User killed);
        protected abstract bool ExecuteWolf(User killed);

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