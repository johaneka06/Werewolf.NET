using System;

namespace Werewolf.NET.Game
{
    public class User
    {
        private Guid _id;
        private string _name;
        private Exp _exp;

        public Guid ID
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

        public int XP
        {
            get
            {
                return this._exp.XP;
            }
        }

        public User()
        {
            this._exp = new Exp();
            this._name = "";
        }

        public User(Guid id, string name, Exp xp)
        {
            this._id = id;
            this._name = name;
            this._exp = xp;
        }

        public static User createUser(string name)
        {
            return new User(Guid.NewGuid(), name, new Exp());
        }

        public void AddEXP(int exp){
            if(exp < 0) throw new Exception("EXP Cannot be negative!");

            this._exp = this._exp.addExp(exp);
        }

        public override bool Equals(object obj)
        {
            var u = obj as User;
            if(u == null) return false;
            
            return u._id.Equals(this._id);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
