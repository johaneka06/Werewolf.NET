using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game{
    public interface IRoleRepository
    {
        List<DBRole> GetAllRoles();

        void Create(Roles newRole);
    }

    public class DBRole
    {
        private int _id;
        private string _name;
        private DateTime _createdDate;

        public int ID
        {
            get
            {
                return this._id;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public DateTime CreatedAt
        {
            get
            {
                return this._createdDate;
            }
        }

        public DBRole(int id, string name, DateTime date)
        {
            this._id = id;
            this._name = name;
            this._createdDate = date;
        }
    }
}