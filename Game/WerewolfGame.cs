using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{

    public class WerewolfVote : Vote
    {
        private User votedPlayer;

        public User Executed
        {
            get
            {
                return this.votedPlayer;
            }
        }
        public WerewolfVote()
        {
            this.votedPlayer = new User();
        }

        public WerewolfVote(User current, User VotedUser) : base(current)
        {
            this.votedPlayer = VotedUser;
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
            UserVoted = new List<User>();
            VoteNumber = new List<int>();
            specialPersonCount = werewolf.Player.Count + seer.Player.Count;
        }
        private WerewolfVote villagerToBeKilled;
        private List<User> UserVoted;
        private List<int> VoteNumber;
        private int maxCount = 0;

        protected override void DoExecute(Vote vote)
        {
            WerewolfVote v = vote as WerewolfVote;

            if (werewolf.Player.Contains(v.Current)) //Werewolf
            {
                villagerToBeKilled = v;
                count++;
            }
            else if (seer.Player.Contains(v.Current)) //Seer job -> know player's role
            {
                if (werewolf.Player.Contains(v.Executed)) Console.WriteLine("Werewolf");
                else Console.WriteLine("Villager");
            }
            else throw new Exception("Invalid move: Villager cannot vote in this session");

            Console.WriteLine("Execute Count: " + count);

            if (count == Werewolf.Player.Count)
            {
                ExecuteVillager(v.Executed);
                Console.WriteLine("Player count: " + totalPlayer);
            }

            if (isLose())
            {
                //If villager lose, then there's no villager left, and only wolf are remains.
                //Logic : When someone executed by wolf, it counts as lose.

                _gameEnded = true;

                foreach (User winner in werewolf.Player)
                {
                    Broadcast(new Win(winner));
                }
            }

        }
        protected override void DoVote(Vote vote)
        {
            WerewolfVote v = vote as WerewolfVote;

            if (!UserVoted.Contains(v.Executed))
            {
                UserVoted.Add(v.Executed);
                VoteNumber.Add(1);
            }
            else
            {
                for (int i = 0; i < VoteNumber.Count; i++)
                {
                    if (UserVoted[i] == v.Executed)
                    {
                        VoteNumber[i]++;
                        break;
                    }
                }
            }

            count++;

            Console.WriteLine("Vote Count: " + count);

            User wastedUser = new User();

            if (count == totalPlayer)
            {
                maxCount = 0;
                for (int i = 0; i < VoteNumber.Count; i++)
                {
                    if (VoteNumber[i] > maxCount)
                    {
                        maxCount = VoteNumber[i];
                        wastedUser = UserVoted[i];
                        Console.WriteLine(wastedUser.Name);
                    }
                }
                string Name = wastedUser.Name;
                if (ExecuteWolf(wastedUser)) Console.WriteLine("You have eliminate wolf: " + Name);
                else Console.WriteLine("Uh. You killed the wrong wolf: " + Name);

                totalPlayer--;
                Console.WriteLine("Player count: " + totalPlayer);

                count = 0;
                UserVoted.Clear();
                VoteNumber.Clear();
            }

            if (isWin()){
                //If win, then there's no wolf remaining. So lose event are put at ExecuteVillager.
                //Logic here: Every player executed, it count as lose. 

                _gameEnded = true;
                
                foreach(User winner in villager.Player){
                    Broadcast(new Win(winner));
                }

                foreach(User winner in seer.Player){
                    Broadcast(new Win(winner));
                }
            }

        }

        protected override void ExecuteVillager(User killed)
        {
            Broadcast(new Lose(killed));

            if (seer.Player.Contains(killed))
            {
                seer.removePlayer(killed);
            }
            else
            {
                villager.removePlayer(killed);
            }

            Console.WriteLine("Player Died: " + killed.Name);

            totalPlayer--;

            count = 0;

            villagerToBeKilled = null;
            killed = null;
        }

        protected override bool ExecuteWolf(User killed)
        {
            Broadcast(new Lose(killed));

            if (Werewolf.Player.Contains(killed))
            {
                werewolf.removePlayer(killed);

                villagerToBeKilled = null;
                return true;
            }

            else if (Seer.Player.Contains(killed)) seer.removePlayer(killed);
            else villager.removePlayer(killed);

            Console.WriteLine("Player Died: " + killed.Name);

            villagerToBeKilled = null;
            killed = null;

            count = 0;

            return false;
        }

        private bool isLose()
        {
            return (villager.Player.Count == 0) ? true : false;
        }

        private bool isWin()
        {
            return (werewolf.Player.Count == 0) ? true : false;
        }

    }
}