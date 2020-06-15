using System;
using Xunit;

using Npgsql;
using Werewolf.NET.Game;
using Werewolf.NET.Game.Database.PostgreSQL;

namespace Test
{
    public class UnitOfWorkTest
    {
        string connection;
        public UnitOfWorkTest()
        {
            connection = "Host=localhost;Username=postgres;Password=postgres;Database=WerewolfDB;Port=5432";
        }

        [Fact]
        public void CreateRoomTest()
        {
            using (var uw = new postgreUnitOfWork(connection))
            {
                Room newRoom = new Room(6);
                uw.RoomRepo.Create(newRoom);
                uw.Commit();

                Room foundRoom = uw.RoomRepo.FindRoom(newRoom.ID);
                Assert.True(foundRoom != null);
            }
        }

        [Fact]
        public void WorkTest()
        {
            using (var uw = new postgreUnitOfWork(connection))
            {
                User ayu, bani, cinta, dita, ester, fina, grace, hanako, ivanka, jean;

                ayu = User.createUser("Ayu");
                uw.UserRepo.Create(ayu);

                bani = User.createUser("Bani");
                uw.UserRepo.Create(bani);

                cinta = User.createUser("Cinta");
                uw.UserRepo.Create(cinta);

                dita = User.createUser("Dita");
                uw.UserRepo.Create(dita);

                ester = User.createUser("Ester");
                uw.UserRepo.Create(ester);

                fina = User.createUser("Fina");
                uw.UserRepo.Create(fina);

                grace = User.createUser("Grace");
                uw.UserRepo.Create(grace);

                hanako = User.createUser("Hanako");
                uw.UserRepo.Create(hanako);

                ivanka = User.createUser("Ivanka");
                uw.UserRepo.Create(ivanka);

                jean = User.createUser("Jean");
                uw.UserRepo.Create(jean);

                Room room = new Room(10);
                uw.RoomRepo.Create(room);

                room.Join(ayu, 1);
                uw.RoomRepo.Join(room, ayu, 1);

                room.Join(bani, 3);
                uw.RoomRepo.Join(room, bani, 3);

                room.Join(cinta, 2);
                uw.RoomRepo.Join(room, cinta, 2);

                room.Join(dita, 2);
                uw.RoomRepo.Join(room, dita, 2);

                room.Join(ester, 2);
                uw.RoomRepo.Join(room, ester, 2);

                room.Join(fina, 1);
                uw.RoomRepo.Join(room, ester, 1);

                room.Join(grace, 2);
                uw.RoomRepo.Join(room, grace, 2);

                room.Join(hanako, 2);
                uw.RoomRepo.Join(room, hanako, 2);

                room.Join(ivanka, 1);
                uw.RoomRepo.Join(room, ivanka, 1);

                room.Join(jean, 2);
                uw.RoomRepo.Join(room, jean, 2);

                room.StartGame("werewolf", uw.UserRepo);
                uw.RoomRepo.ChangeGame(room, room.Game);

                Vote vote;

                //Night
                vote = new WerewolfVote(ayu.ID, bani.ID);
                room.Execute(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = new WerewolfVote(fina.ID, bani.ID);
                room.Execute(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = new WerewolfVote(ivanka.ID, bani.ID);
                room.Execute(vote);
                uw.RoomRepo.AddVote(room, vote);

                //Day
                vote = (new WerewolfVote(ayu.ID, cinta.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(cinta.ID, ayu.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(dita.ID, ayu.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(ester.ID, ayu.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(fina.ID, ayu.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(grace.ID, ayu.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(hanako.ID, ayu.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(ivanka.ID, ayu.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(jean.ID, ayu.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                //Night
                vote = (new WerewolfVote(fina.ID, dita.ID));
                room.Execute(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(ivanka.ID, dita.ID));
                room.Execute(vote);
                uw.RoomRepo.AddVote(room, vote);

                //Day
                vote = (new WerewolfVote(cinta.ID, fina.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(ester.ID, fina.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(fina.ID, ester.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(grace.ID, fina.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(hanako.ID, fina.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(ivanka.ID, fina.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(jean.ID, fina.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                //Night
                vote = (new WerewolfVote(ivanka.ID, grace.ID));
                room.Execute(vote);
                uw.RoomRepo.AddVote(room, vote);

                //Day
                vote = (new WerewolfVote(cinta.ID, ivanka.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(ester.ID, ivanka.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(hanako.ID, ivanka.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(ivanka.ID, hanako.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                vote = (new WerewolfVote(jean.ID, ivanka.ID));
                room.Vote(vote);
                uw.RoomRepo.AddVote(room, vote);

                Assert.Equal(8, uw.UserRepo.FindById(jean.ID).XP);
                Assert.Equal(4, uw.UserRepo.FindById(fina.ID).XP);
            }
        }

    }
}