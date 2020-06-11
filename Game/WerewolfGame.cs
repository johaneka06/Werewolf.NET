using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{

    public class WerewolfVote : Vote
    {
        private int totalVote;

        public int VoteCount
        {
            get
            {
                return totalVote;
            }
        }

        public WerewolfVote updateVote()
        {
            return new WerewolfVote(currentPlayer, votedPlayer, this.totalVote + 1);
        }

        public WerewolfVote() { }

        public WerewolfVote(User current, User VotedUser, int totalVote) : base(current, VotedUser)
        {
            this.totalVote = totalVote;
        }
    }

    public class WerewolfGame : WolfNet
    {
        public WerewolfGame(List<User> players, List<int> UserRoles) : base(players, UserRoles) { }

        protected override void Init()
        {
            if (this._players.Count < 5) throw new Exception("Player must be 5 or more!");
            else if (this._players.Count != this._userRoles.Count) throw new Exception("Invalid assign role: Not every player is assigned with their roles!");

            int idx = 0;

            foreach (int RoleIdx in _userRoles)
            {
                if (RoleIdx == 1) werewolf.AddPlayer(_players[idx]);
                else if (RoleIdx == 2) villager.AddPlayer(_players[idx]);
                else if (RoleIdx == 3) seer.AddPlayer(_players[idx]);
                idx++;
            }
            villagerToBeKilled = new WerewolfVote();
            wolfToBeKilled = new List<WerewolfVote>();
            specialPersonCount = werewolf.Player.Count + seer.Player.Count;
        }
        private WerewolfVote villagerToBeKilled;
        private List<WerewolfVote> wolfToBeKilled;

        protected override bool DoVote(Vote vote)
        {
            WerewolfVote v = vote as WerewolfVote;

            if (isNight && specialPersonCount > 0) //Time for special person do their job
            {
                if (werewolf.Player.Contains(v.Current)) //Werewolf
                {
                    if (villagerToBeKilled.Executed == v.Executed) villagerToBeKilled = v.updateVote();
                    else villagerToBeKilled = v;
                    specialPersonCount--;
                }
                else if (seer.Player.Contains(v.Current)) //Seer job -> know player's role
                {
                    if (werewolf.Player.Contains(v.Executed)) Console.WriteLine("Werewolf");
                    else Console.WriteLine("Villager");
                    specialPersonCount--;
                }
                else throw new Exception("Villager aren't eligible to vote!");
            }
            else if (!isNight) //Time for vote to kill whether the wolf, seer, or villager
            {
                int maxCount = 0;

                for (int i = 0; i < wolfToBeKilled.Count; i++)
                {
                    if (wolfToBeKilled[i] == v)
                    {
                        wolfToBeKilled[i] = wolfToBeKilled[i].updateVote();
                    }

                    if (wolfToBeKilled[i].VoteCount > maxCount) villagerToBeKilled = wolfToBeKilled[i];
                }
            }

            if (specialPersonCount == 0 && isNight)
            {
                ExecuteVillager();
                specialPersonCount = werewolf.Player.Count + seer.Player.Count;
            }
            else if (!isNight)
            {
                ExecuteWolf();
            }

            if (!isNight && isWin()) return true;
            else if (isNight && isLose()) return false;

            return false;
        }

        protected override void ExecuteVillager()
        {
            villagerToBeKilled.Executed.AddEXP(4);
            if (seer.Player.Contains(villagerToBeKilled.Executed))
            {
                seer.removePlayer(villagerToBeKilled.Executed);
            }
            else
            {
                villager.removePlayer(villagerToBeKilled.Executed);
            }

            villagerToBeKilled = null;
        }

        protected override bool ExecuteWolf()
        {
            User killed = villagerToBeKilled.Executed;
            killed.AddEXP(4);

            if (Werewolf.Player.Contains(villagerToBeKilled.Executed))
            {
                werewolf.removePlayer(villagerToBeKilled.Executed);

                villagerToBeKilled = null;
                return true;
            }
            else if (Seer.Player.Contains(villagerToBeKilled.Executed)) seer.removePlayer(villagerToBeKilled.Executed);
            else villager.removePlayer(villagerToBeKilled.Executed);

            return false;
        }

        protected override void giveExp()
        {
            //If Lose -> Villager died
            if (checkAnyWolf())
            {
                foreach (User player in werewolf.Player)
                {
                    player.AddEXP(8);
                }
                foreach (User player in villager.Player)
                {
                    player.AddEXP(4);
                }
                foreach (User player in seer.Player)
                {
                    player.AddEXP(4);
                }
            }
            else if (_gameEnded && isWin())
            {
                foreach (User player in werewolf.Player)
                {
                    player.AddEXP(4);
                }
                foreach (User player in villager.Player)
                {
                    player.AddEXP(8);
                }
                foreach (User player in seer.Player)
                {
                    player.AddEXP(8);
                }
            }
        }

        private bool isLose()
        {
            return checkAnyVilager();
        }

        private bool checkAnyVilager()
        {
            return (villager.Player.Count == 0) ? true : false;
        }

        private bool isWin()
        {
            return checkAnyWolf();
        }

        private bool checkAnyWolf()
        {
            return (werewolf.Player.Count == 0) ? true : false;
        }
    }
}