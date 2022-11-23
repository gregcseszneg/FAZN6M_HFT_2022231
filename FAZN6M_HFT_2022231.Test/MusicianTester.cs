using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using FAZN6M_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FAZN6M_HFT_2022231.Test
{
    [TestFixture]
    public class MusicianTester
    {
        MusicianLogic logic;
        Mock<IRepository<Musician>> mockMusicianRepo;
        [SetUp]
        public void InIt()
        {
            mockMusicianRepo = new Mock<IRepository<Musician>>();
            mockMusicianRepo.Setup(m => m.ReadAll()).Returns(new List<Musician>()
            {
                new Musician("1#Pop Smoke#07/20/1999#New York City#USA#male#2"),
                new Musician("2#Charlie Puth#12/02/1991#Rumson#USA#male#1"),
                new Musician("3#Tyga#11/19/1998#Compton#USA#male#3"),
                new Musician("4#Travis Scott#04/30/1991#Houston#USA#male#3"),
                new Musician("5#Joyner Lucas#08/17/1988#Worcester#USA#male#")
            }.AsQueryable());

            logic = new MusicianLogic(mockMusicianRepo.Object);
        }
        [Test]
        public void CheckCreate()
        {
            //ARRANGE
            Musician newMusician = new Musician("6#Juice Wrld#12/02/1998#Chicago#USA#male#");
        
            //ACT
            logic.Create(newMusician);

            //ASSERT
            mockMusicianRepo.Verify(x => x.Create(newMusician), Times.Once);
        }

        [Test]
        public void CheckCreateNameNull()
        {
            //ARRANGE
            Musician newMusician = new Musician("6##12/02/1998#Chicago#USA#male#");

            //ASSERT
            Assert.That(() => logic.Create(newMusician), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void CheckCreateIncorrectAge()
        {
            //ARRANGE
            Musician newMusician = new Musician("6#Juice Wrld#12/02/1888#Chicago#USA#male#");

            //ASSERT
            Assert.That(() => logic.Create(newMusician), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void CheckDelete()
        {
            //ACT
            logic.Delete(1);

            //ASSERT
            mockMusicianRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void CheckUpdate()
        {
            //ARRANGE
            Musician newTravis =new Musician("4#Travis Scott#04/29/1991#Budapest#Hungary#male#3");

            //ACT
            logic.Update(newTravis);

            //ASSERT
            mockMusicianRepo.Verify(x => x.Update(newTravis), Times.Once);
        }
        [Test]
        public void CheckRead()
        {
            //ARRANGE
            Musician tyga = new Musician("3#Tyga#11/19/1998#Compton#USA#male#3");
            mockMusicianRepo.Setup(m => m.Read(2)).Returns(tyga);

            //ACT
            var result = logic.Read(2);
            
            //ASSERT
            Assert.That(result, Is.EqualTo(tyga));
        }
        [Test]
        public void CheckReadAll()
        {
            //ARRANGE
            var shouldbe = new List<Musician>()
            {
                new Musician("1#Pop Smoke#07/20/1999#New York City#USA#male#2"),
                new Musician("2#Charlie Puth#12/02/1991#Rumson#USA#male#1"),
                new Musician("3#Tyga#11/19/1998#Compton#USA#male#3"),
                new Musician("4#Travis Scott#04/30/1991#Houston#USA#male#3"),
                new Musician("5#Joyner Lucas#08/17/1988#Worcester#USA#male#")
            }.AsEnumerable();

            //ACT
            var result = logic.ReadAll();

            //ASSERT
            Assert.That(result, Is.EqualTo(shouldbe));
        }
    }
}
