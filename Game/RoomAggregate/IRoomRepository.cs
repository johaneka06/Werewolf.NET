using System;

namespace Werewolf.NET.Game
{
    public interface IRoomRepository
    {
        void Create(Room room);
        void Join(Room room, User player, int RoleId);
        void UpdateMax(Room room, int max);
        Room FindRoom(Guid id);
        void Close(Room room);

        void AddVote(Room room, Vote vote);
    }
}