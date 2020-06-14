using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{
    public class GameFactory
    {
        public static WolfNet Create(string gameName, List<User> player, List<int> role)
        {
            if (gameName.Equals("werewolf"))
            {
                if (player.Count < 5) throw new Exception("Player must be 5 or more!");

                WolfNet game = new WerewolfGame(player, role);

                WerewolfResultHandler win = new WinHandler(new WerewolfWin());
                WerewolfResultHandler lose = new LoseHandler(new WerewolfLose());

                game.Attach(win);
                game.Attach(lose);

                return game;
            }

            throw new Exception("Game not found!");
        }
    }
}