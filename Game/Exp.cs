using System;

namespace Werewolf.NET.Game
{
    public class Exp
    {
        private int _exp;

        public int XP
        {
            get
            {
                return _exp;
            }
        }

        public Exp()
        {
            this._exp = 0;
        }

        public Exp(int exp)
        {
            if (exp < 0) throw new Exception("EXP cannot be negative!");

            this._exp = exp;
        }

        public Exp addExp(int exp)
        {
            if (exp < 0) throw new Exception("EXP cannot be negative!");
            return new Exp(this._exp + exp);
        }

        public override bool Equals(object obj)
        {
            var ob = obj as Exp;
            if (ob == null) return false;

            return ob._exp == this._exp;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}