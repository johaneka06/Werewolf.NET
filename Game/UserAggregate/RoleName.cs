using System;

namespace Werewolf.NET.Game
{
    public class RoleName{
        private string _name;

        public string Name{
            get{
                return this._name;
            }
        }

        public RoleName(){
            this._name = "";
        }

        public RoleName(string name){
            if (name.Length < 3) throw new Exception("Role name must be 3 chars or more");

            this._name = name;
        }

        public RoleName updateRoleName(string role)
        {
            if (role.Length < 3) throw new Exception("Role name must be 3 chars or more");

            return new RoleName(role);
        }

        public override bool Equals(object obj)
        {
            var rn = obj as RoleName;
            if(rn == null) return false;
            
            return this._name == rn._name;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}