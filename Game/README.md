# Game

1. Exp: Value object for user's exp

2. RoleName: Value object for every role name

3. Roles: Entity that used for storing roles and user who get that role in game

4. Room: Entity that used for playing room

5. User: Entity that used for user data such as his/her name and exp

6. Werewolf: Game

7. WolfNet: Abstract class for game

## Logic

At WolfNet.cs, there's a list of integer that will be used to assign the role of each player. At WerewolfGame.cs, each element of this list are used to assign player based on their role.

Each player is considered lose if player is executed by wolf at night or if player is executed by villager at noon.

## Notes

This notes is written in Bahasa Indonesia.

### Connection at domain layer - Fixed

Before:

[Link](https://github.com/johaneka06/Werewolf.NET/blob/6_Event-Sourcing/Game/RoomAggregate/WerewolfGame.cs#L73)

Haram domain layer pegang connection. domain layer cuma boleh pegang repo.

After:

[Link](https://github.com/johaneka06/Werewolf.NET/blob/master/Game/RoomAggregate/WerewolfGame.cs#L73)

### Smell primitive obsession

[Link1](https://github.com/johaneka06/Werewolf.NET/blob/master/Game/RoomAggregate/WerewolfGame.cs#L40)

[Link2](https://github.com/johaneka06/Werewolf.NET/blob/master/Game/UserAggregate/RoleName.cs)

RoleName smell primitive obsession nih. bagusnya lu bikin abstract class Role terus di-extends jadi 3 role itu. buat convert dari string menjadi concrete object, bisa pake if-else string tapi dibuat di dalem factory. jadi misal if(role == "werewolf") { return new Werewolf(...) }
