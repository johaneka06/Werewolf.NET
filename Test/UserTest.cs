using System;
using Xunit;
using Werewolf.NET.Game;

namespace Test
{
    public class UserTest
    {
        User u;

        public UserTest()
        {
            u = User.createUser("John Doe");
        }

        [Fact]
        public void CreateUser()
        {
            User us = User.createUser("John Doe");
            Assert.Equal("John Doe", us.Name);
            Assert.Equal(0, us.XP);
        }

        [Fact]
        public void AddEXP()
        {
            Assert.Equal(0, u.XP);

            u.AddEXP(5);
            Assert.Equal(5, u.XP);

            Exception ex = null;
            try
            {
                u.AddEXP(-1);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.True(ex != null);
        }
    }
}
