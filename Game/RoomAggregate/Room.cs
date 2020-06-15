using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{
    public class Room
    {
        private Guid _id;
        private List<Guid> _player;
        private List<int> _roles;
        private WolfNet _game;
        private int _maxUser;

        public Guid ID
        {
            get
            {
                return this._id;
            }
        }

        public List<Guid> Players
        {
            get
            {
                return this._player;
            }
        }

        public int MaxUser
        {
            get
            {
                return this._maxUser;
            }
        }

        public WolfNet Game
        {
            get
            {
                return this._game;
            }
            set
            {
                this.Game = value;
            }
        }

        public Room(Guid id, int Max)
        {
            this._id = id;
            this._maxUser = Max;
            this._player = new List<Guid>();
            this._roles = new List<int>();
        }

        public Room(int max)
        {
            if (max < 1) throw new Exception("Max must be more than 1!");

            this._id = Guid.NewGuid();
            this._maxUser = max;
            this._player = new List<Guid>();
            this._roles = new List<int>();
            this._game = null;
        }

        public void Join(User player, int role)
        {
            if (_game != null) throw new Exception("Game already started");
            else if (_player.Count < _maxUser)
            {
                _player.Add(player.ID);
                _roles.Add(role);
            }
            else if (_player.Count >= _maxUser)
            {
                throw new Exception("Max player reached");
            }
        }

        public void StartGame(string game, IUserRepository userRepo = null)
        {
            _game = GameFactory.Create(game, _player, _roles, userRepo, "");
        }

        public void Execute(Vote vote)
        {
            if (_game == null) throw new Exception("Game isn't exist");

            _game.Execute(vote);
        }

        public void Vote(Vote vote)
        {
            if (_game == null) throw new Exception("Game isn't exist");

            _game.Vote(vote);
        }

        public override bool Equals(object obj)
        {
            var r = obj as Room;
            if (r == null) return false;

            return this._id.Equals(r._id);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}