# Werewolf.NET

![License](http://img.shields.io/:license-mit-green.svg?style=flat-square)

This repository is used for DDD building block training purpose.

## Getting Started

Werewolf is a social game that takes place over a series of game days.

Players should sit in a circle, preferably around a table. The Moderator will deal out a role card to each player. The players do not share these roles with each other.

Roles in this game are:

1. Villager:

The most commonplace role, a simple Villager, spends the game trying to root out who they believe the werewolves (and other villagers) are. While they do not need to lie, the role requires players to keenly sense and point out the flaws or mistakes of their fellow players. Someone is speaking too much? Could mean they're a werewolf. Someone isn't speaking enough? Could mean the same thing. It all depends on the people you're playing with, and how well you know them.

2. Wolf:

Typically werewolves are outnumbered by villagers 2 to 1. So a game of 6 players would have 2 werewolves. The goal of the werewolves is to decide together on one villager to secretly kill off during the night, while posing as villagers during the day so they're not killed off themselves. One by one they'll kill off villagers and win when there are either the same number of villagers and werewolves left, or all the villagers have died. This role is the hardest of all to maintain, because these players are lying for the duration of the game.

3. Seer:

The Seer, while first and foremost a villager, has the added ability to "see" who the werewolves are once night falls. When called awake by the Moderator, the Seer can point to any of their fellow players and the Moderator must nod yes or no as to whether or not they are indeed a Werewolf. The Seer can then choose to keep this information a secret during the day, or reveal themselves as the Seer and use the knowledge they gained during the night in their defense or to their advantage during the day. The strategy here is up to you.

### Prerequisities

To run this backend of werewolf game, you need [```.NET Core SDK```](https://dotnet.microsoft.com/download). You can install them by following instruction:

```
1. Download .NET Core SDK at [here](https://dotnet.microsoft.com/download).

2. Install the .NET Core SDK at your computer using the installer you download.
```

## Automated Test

This project also include the automated test file that test every aspect of this game.

### Running The Test

Steps on automated test run:

1. Open ```command prompt``` or ```terminal``` and change the directory become ```Test``` directory.

2. Use syntax ```dotnet build``` to build the automated test if you haven't build it.

3. Use syntax ```dotnet test``` to test the testing unit that available

## Built with

[.NET](https://docs.microsoft.com/en-us/dotnet/) - Core Project

## Author

**Johan Eka Santosa** - *Initial Work* - [johaneka06](https://github.com/johaneka06)

## License

This project is licensed under MIT License - see the LICENSE file for details.