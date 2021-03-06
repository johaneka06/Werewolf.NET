using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{
    public class Roles
    {
        private int _id;
        private RoleName _name;
        private List<Guid> _player;

        public int ID
        {
            get
            {
                return this._id;
            }
        }

        public string RoleName
        {
            get
            {
                return this._name.Name;
            }
        }

        public List<Guid> Player
        {
            get
            {
                return this._player;
            }
        }

        public Roles()
        {
            this._id = -1;
            this._name = null;
            this._player = new List<Guid>();
        }

        public Roles(int id, RoleName name)
        {
            this._id = id;
            this._name = name;
            this._player = new List<Guid>();
        }

        public static Roles createRole(int id, string name)
        {
            RoleName rn = new RoleName(name);
            return new Roles(id, rn);
        }

        public void AddPlayer(Guid player)
        {
            this._player.Add(player);
        }

        public void removePlayer(Guid player)
        {
            this._player.Remove(player);
        }

        public override bool Equals(object obj)
        {
            var role = obj as Roles;
            if (role == null) return false;

            return role._id == this._id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}