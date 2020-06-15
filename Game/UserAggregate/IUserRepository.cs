using System;
using System.Collections.Generic;

namespace Werewolf.NET.Game
{
    public interface IUserRepository
    {
        User FindById(Guid id);
        void Create(User user);
        void AddExp(User user, int EXP);
        List<User> GetAllUser();
    }
}