using System;

namespace Werewolf.NET.Game
{
    public abstract class WerewolfResultHandler : IObserver<WerewolfResult>
    {
        protected IExpGainer _gainer;

        public WerewolfResultHandler(IExpGainer gainer)
        {
            this._gainer = gainer;
        }

        public abstract void Update(WerewolfResult e);
    }

    public class WinHandler : WerewolfResultHandler
    {
        public WinHandler(IExpGainer gainer) : base(gainer) { }

        public override void Update(WerewolfResult e)
        {
            Win evnt = e as Win;
            if (evnt == null) return;

            evnt.Player.AddEXP(_gainer.Gain());
        }
    }

    public class LoseHandler : WerewolfResultHandler
    {
        public LoseHandler(IExpGainer gainer) : base(gainer) { }

        public override void Update(WerewolfResult e)
        {
            Lose evnt = e as Lose;
            if (evnt == null) return;

            evnt.Player.AddEXP(_gainer.Gain());
        }
    }
}