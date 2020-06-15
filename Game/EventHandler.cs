using System;

namespace Werewolf.NET.Game
{
    public abstract class WerewolfResultHandler : IObserver<WerewolfResult>
    {
        protected string connectionStr;
        protected IUserRepository repo;
        protected IExpGainer _gainer;

        public WerewolfResultHandler(IUserRepository reposit, IExpGainer gainer)
        {
            this._gainer = gainer;
            repo = reposit;
        }

        public abstract void Update(WerewolfResult e);
    }

    public class WinHandler : WerewolfResultHandler
    {
        public WinHandler(IUserRepository reposit, IExpGainer gainer) : base(reposit, gainer) { }

        public override void Update(WerewolfResult e)
        {
            if(repo == null) return;

            Win evnt = e as Win;
            if (evnt == null) return;

            User player = repo.FindById(evnt.Player);

            repo.AddExp(player, _gainer.Gain());
        }
    }

    public class LoseHandler : WerewolfResultHandler
    {
        public LoseHandler(IUserRepository repository, IExpGainer gainer) : base(repository, gainer) { }

        public override void Update(WerewolfResult e)
        {
            if(repo == null) return;

            Lose evnt = e as Lose;
            if (evnt == null) return;

            User player = repo.FindById(evnt.Player);
            repo.AddExp(player, _gainer.Gain());
        }
    }
}