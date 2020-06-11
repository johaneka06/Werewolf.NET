using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{
    public abstract class WolfNet
    {
        protected bool _gameEnded;

        protected List<User> _players;
        protected List<int> _userRoles;
        protected Roles werewolf;
        protected Roles villager;
        protected Roles seer;
        protected bool isNight;
        protected int specialPersonCount;

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

        public WolfNet(List<User> players, List<int> UserRoles)
        {
            this.werewolf = new Roles(1, new RoleName("Werewolf"));
            this.villager = new Roles(2, new RoleName("Villager"));
            this.seer = new Roles(3, new RoleName("Seer"));

            this._players = players;
            this._userRoles = UserRoles;
            this._gameEnded = false;
            this.isNight = true;

            this.Init();
        }

        public bool Vote(Vote vote)
        {
            if (_gameEnded) throw new Exception("Game already ended");

            bool isEnded = DoVote(vote);

            if (!isEnded)
            {
                isNight = !isNight;
                return isEnded;
            }
            _gameEnded = true;

            this.giveExp();

            return isEnded;
        }

        protected abstract void Init();

        protected abstract bool DoVote(Vote vote);
        protected abstract void ExecuteVillager();
        protected abstract bool ExecuteWolf();
        protected abstract void giveExp();
    }

    public abstract class Vote
    {
        protected User currentPlayer;
        protected User votedPlayer;

        public User Executed
        {
            get
            {
                return this.votedPlayer;
            }
        }

        public User Current
        {
            get
            {
                return this.currentPlayer;
            }
        }

        public Vote(User current, User vote)
        {
            this.currentPlayer = current;
            this.votedPlayer = vote;
        }

        public Vote()
        {
            this.votedPlayer = new User();
            this.currentPlayer = new User();
        }

    }

}