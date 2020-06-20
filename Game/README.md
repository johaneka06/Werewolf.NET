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

Each player is considered lose if player is executed by wolf at night or if player is executed by villager at noon
