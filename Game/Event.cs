using System;

namespace Werewolf.NET.Game
{
    public abstract class WerewolfResult
    {
        public User Player { get; private set; }
        public WerewolfResult(User player)
        {
            this.Player = player;
        }
    }

    public class Win : WerewolfResult
    {
        public Win(User player) : base(player) { }
    }

    public class Lose : WerewolfResult
    {
        public Lose(User player) : base(player) { }
    }
}