using System;

namespace Werewolf.NET.Game
{
    public class WerewolfWin : IExpGainer
    {
        public int Gain()
        {
            return 8;
        }
    }

    public class WerewolfLose : IExpGainer
    {
        public int Gain()
        {
            return 4;
        }
    }
}