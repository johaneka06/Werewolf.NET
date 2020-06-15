using System;

namespace Werewolf.NET.Game
{
    public abstract class WerewolfResult
    {
        public Guid Player { get; private set; }
        public WerewolfResult(Guid player)
        {
            this.Player = player;
        }
    }

    public class Win : WerewolfResult
    {
        public Win(Guid player) : base(player) { }
    }

    public class Lose : WerewolfResult
    {
        public Lose(Guid player) : base(player) { }
    }
}