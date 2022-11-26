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
        MusicianLogic mLogic;
        TrackLogic tLogic;
        RecordLabelLogic rLogic;

        Mock<IRepository<Musician>> mockMusicianRepo;
        Mock<IRepository<Track>> mockTrackRepo;
        Mock<IRepository<RecordLabel>> mockRecordLabelRepo;
        [SetUp]
        public void InIt()
        {
            var musicians = new List<Musician>()
            {
                new Musician("1#Pop Smoke#07/20/1999#New York City#USA#male#2"),
                new Musician("2#Charlie Puth#12/02/1991#Rumson#USA#male#1"),
                new Musician("3#Tyga#11/19/1998#Compton#USA#male#3"),
                new Musician("4#Travis Scott#04/30/1991#Houston#USA#male#3"),
                new Musician("5#Russ#09/26/1992#Atlanta#USA#male#")
            }.AsQueryable();

            var tracks = new List<Track>()
            {
                new Track("1#Are You Entertained#156#5#"),
                new Track("2#CIVIL WAR#144#5#1"),
                new Track("3#GUESS WHAT#206#5#1"),
                new Track("4#Dior#216#1#"),
                new Track("5#See You Again#229#2#"),
                new Track("6#Chosen#161#3#"),
                new Track("7#HIGHEST IN THE ROOM#175#4#"),
            }.AsQueryable();

            var recordlabels = new List<RecordLabel>()
            {
                new RecordLabel("1#Capitol Records#1942#USA#Hollywood"),
                new RecordLabel("2#RCA Records#1900#USA#New York City"),
                new RecordLabel("3#Death Row Records#1991#USA#Beverly Hills")
            }.AsQueryable();

            mockMusicianRepo = new Mock<IRepository<Musician>>();
            mockMusicianRepo.Setup(m => m.ReadAll()).Returns(musicians);

            mockTrackRepo = new Mock<IRepository<Track>>();
            mockTrackRepo.Setup(m => m.ReadAll()).Returns(tracks);

            mockRecordLabelRepo = new Mock<IRepository<RecordLabel>>();
            mockRecordLabelRepo.Setup(m => m.ReadAll()).Returns(recordlabels);

            mLogic = new MusicianLogic(mockMusicianRepo.Object);
            tLogic = new TrackLogic(mockTrackRepo.Object);
            rLogic = new RecordLabelLogic(mockRecordLabelRepo.Object);
        }
        [Test]
        public void CheckCreate()
        {
            //ARRANGE
            Musician newMusician = new Musician("6#Juice Wrld#12/02/1998#Chicago#USA#male#");
        
            //ACT
            mLogic.Create(newMusician);

            //ASSERT
            mockMusicianRepo.Verify(x => x.Create(newMusician), Times.Once);
        }

        [Test]
        public void CheckCreateNameNull()
        {
            //ARRANGE
            Musician newMusician = new Musician("6##12/02/1998#Chicago#USA#male#");

            //ASSERT
            Assert.That(() => mLogic.Create(newMusician), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void CheckCreateIncorrectAge()
        {
            //ARRANGE
            Musician newMusician = new Musician("6#Juice Wrld#12/02/1888#Chicago#USA#male#");

            //ASSERT
            Assert.That(() => mLogic.Create(newMusician), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void CheckDelete()
        {
            //ACT
            mLogic.Delete(1);

            //ASSERT
            mockMusicianRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void CheckUpdate()
        {
            //ARRANGE
            Musician newTravis =new Musician("4#Travis Scott#04/29/1991#Budapest#Hungary#male#3");

            //ACT
            mLogic.Update(newTravis);

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
            var result = mLogic.Read(2);
            
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
                new Musician("5#Russ#09/26/1992#Atlanta#USA#male#")
            }.AsEnumerable();

            //ACT
            var result = mLogic.ReadAll();

            //ASSERT
            Assert.That(result, Is.EqualTo(shouldbe));
        }
    }
}
