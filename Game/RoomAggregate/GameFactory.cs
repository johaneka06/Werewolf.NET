using System;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Werewolf.NET.Game
{
    public class GameFactory
    {
        public static WolfNet Create(string gameName, List<Guid> player, List<int> role, IUserRepository userRepo = null, string lastState = "")
        {
            if (gameName.Equals("werewolf"))
            {
                if (player.Count < 5) throw new Exception("Player must be 5 or more!");

                WolfNet game = new WerewolfGame(player, role);

                WerewolfResultHandler win = new WinHandler(userRepo, new WerewolfWin());
                WerewolfResultHandler lose = new LoseHandler(userRepo, new WerewolfLose());

                game.Attach(win);
                game.Attach(lose);

                if(lastState == null || lastState == "") return game;

                if(gameName.Equals("werewolf"))
                {
                    WerewolfMemento memento = JsonSerializer.Deserialize<WerewolfMemento>(lastState);
                    game.LoadMemento(memento);
                }

                return game;
            }

            throw new Exception("Game not found!");
        }
    }
}