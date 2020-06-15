using System;
using System.Collections.Generic;
using Npgsql;

using Werewolf.NET.Game.Database.PostgreSQL;

namespace Werewolf.NET.Game
{

    public class WerewolfVote : Vote
    {
        private Guid votedPlayer;

        public Guid Executed
        {
            get
            {
                return this.votedPlayer;
            }
        }

        public WerewolfVote()
        {
            currentPlayer = new Guid();
            this.votedPlayer = new Guid();
        }

        public WerewolfVote(Guid current, Guid VotedUser) : base(current)
        {
            this.votedPlayer = VotedUser;
        }
    }

    public class WerewolfGame : WolfNet
    {
        protected bool _gameEnded;
        protected int totalPlayer;
        public WerewolfGame(List<Guid> players, List<int> UserRoles)
        {
            werewolf = new Roles(1, new RoleName("Werewolf"));
            villager = new Roles(2, new RoleName("Villager"));
            seer = new Roles(3, new RoleName("Seer"));

            _players = players;
            _userRoles = UserRoles;

            if (_players.Count < 5) throw new Exception("Player must be 5 or more!");
            else if (_players.Count != this._userRoles.Count) throw new Exception("Invalid assign role: Not every player is assigned with their roles!");

            _gameEnded = false;
            isNight = true;
            count = 0;
            totalPlayer = _players.Count;

            int idx = 0;
            foreach (int RoleIdx in _userRoles)
            {
                if (RoleIdx == 1) werewolf.AddPlayer(_players[idx]);
                else if (RoleIdx == 2) villager.AddPlayer(_players[idx]);
                else if (RoleIdx == 3) seer.AddPlayer(_players[idx]);
                idx++;
            }

            villagerToBeKilled = new WerewolfVote();
            UserVoted = new List<Guid>();
            VoteNumber = new List<int>();

            string connectionStr = "Host=localhost;Username=postgres;Password=postgres;Database=WerewolfDB;Port=5432";

            _connection = new NpgsqlConnection(connectionStr);
            UserRepo = new UserRepository(_connection, null);
        }
        private NpgsqlConnection _connection;
        private WerewolfVote villagerToBeKilled;
        private List<Guid> UserVoted;
        private List<int> VoteNumber;
        private IUserRepository UserRepo;
        private int maxCount = 0;

        public override void Execute(Vote vote)
        {
            WerewolfVote v = vote as WerewolfVote;
            if (isNight)
            {
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

                    foreach (Guid winner in werewolf.Player)
                    {
                        Broadcast(new Win(winner));
                    }
                }
            }
            else throw new Exception("Cannot execute in day!");
        }
        private Guid currentId;
        public override void Vote(Vote vote)
        {
            WerewolfVote v = vote as WerewolfVote;

            currentId = v.Current;

            if (!isNight)
            {
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

                Guid wastedUser = new Guid();

                if (count == totalPlayer)
                {
                    maxCount = 0;
                    for (int i = 0; i < VoteNumber.Count; i++)
                    {
                        if (VoteNumber[i] > maxCount)
                        {
                            maxCount = VoteNumber[i];
                            wastedUser = UserVoted[i];
                            Console.WriteLine(wastedUser);
                        }
                    }
                    _connection.Open();
                    User newUser = UserRepo.FindById(wastedUser);
                    string Name = "";
                    if(newUser != null) Name = newUser.Name; //Find via repo
                    if (ExecuteWolf(wastedUser)) Console.WriteLine("You have eliminate wolf: " + wastedUser + " - " + Name);
                    else Console.WriteLine("Uh. You killed the wrong wolf: " + wastedUser + " - " + Name);

                    totalPlayer--;
                    Console.WriteLine("Player count: " + totalPlayer);

                    count = 0;
                    UserVoted.Clear();
                    VoteNumber.Clear();

                    _connection.Close();
                }

                if (isWin())
                {
                    //If win, then there's no wolf remaining. So lose event are put at ExecuteVillager.
                    //Logic here: Every player executed, it count as lose. 

                    _gameEnded = true;

                    foreach (Guid winner in villager.Player)
                    {
                        Broadcast(new Win(winner));
                    }

                    foreach (Guid winner in seer.Player)
                    {
                        Broadcast(new Win(winner));
                    }
                }
            }
            else throw new Exception("Cannot vote in midnight!");
        }

        //Execute villager means it is night. The werewolf execute other player
        protected override void ExecuteVillager(Guid killed)
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

            Console.WriteLine("Player Died: " + killed);

            totalPlayer--;

            count = 0;

            villagerToBeKilled = null;
            killed = new Guid();

            isNight = !isNight;
        }

        //ExecuteWolf means it is day. Players do their vote to determine which player is the wolf
        protected override bool ExecuteWolf(Guid killed)
        {
            Broadcast(new Lose(killed));

            isNight = !isNight;

            if (Werewolf.Player.Contains(killed))
            {
                werewolf.removePlayer(killed);

                villagerToBeKilled = null;
                return true;
            }

            else if (Seer.Player.Contains(killed)) seer.removePlayer(killed);
            else villager.removePlayer(killed);

            Console.WriteLine("Player Died: " + killed);

            villagerToBeKilled = null;
            killed = new Guid();

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

        public override string GetGameName()
        {
            return "Werewolf";
        }

        public override object GetMemento()
        {
            return new WerewolfMemento(_players, _userRoles, currentId, _gameEnded);
        }

        public override void LoadMemento(object memento)
        {
            var m = memento as WerewolfMemento;
            if (m == null) throw new Exception("Wrong memento");

            this.currentId = m.currentPlayer;
            this._gameEnded = m.GameEnded;
            this._userRoles = m.Roles;
            this._players = m.Player;
        }
    }

    public class WerewolfMemento
    {
        public bool GameEnded { get; set; }
        public List<Guid> Player { get; set; }
        public List<int> Roles { get; set; }
        public Guid currentPlayer { get; set; }

        public WerewolfMemento() { }
        public WerewolfMemento(List<Guid> player, List<int> role, Guid currentPlayer, bool gameEnded)
        {
            this.GameEnded = gameEnded;
            this.Player = player;
            this.Roles = role;
            this.currentPlayer = currentPlayer;
        }
    }
}