using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using FAZN6M_HFT_2022231.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FAZN6M_HFT_2022231.Test
{
    [TestFixture]
    public class Tester
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
            var recordLabels = new List<RecordLabel>()
            {
                new RecordLabel("1#Capitol Records#1942#USA#Hollywood"),
                new RecordLabel("2#RCA Records#1900#USA#New York City"),
                new RecordLabel("3#Death Row Records#1991#USA#Beverly Hills")
            };

            var musicians = new List<Musician>()
            {
                new Musician("1#Pop Smoke#07/20/1999#New York City#USA#male#2"){RecordLabel = recordLabels[1]},
                new Musician("2#Charlie Puth#12/02/1991#Rumson#USA#male#1"){RecordLabel = recordLabels[0]},
                new Musician("3#Tyga#11/19/1998#Compton#USA#male#3"){RecordLabel = recordLabels[2]},
                new Musician("4#Travis Scott#04/30/1991#Houston#USA#male#3"){RecordLabel = recordLabels[2]},
                new Musician("5#Russ#09/26/1992#Atlanta#USA#male#3") { RecordLabel = recordLabels[2] }
            };

            var tracks = new List<Track>()
            {
                new Track("1#Are You Entertained#156#5#"){Musician = musicians[4]},
                new Track("2#CIVIL WAR#144#5#1"){Musician = musicians[4]},
                new Track("3#GUESS WHAT#206#5#1"){Musician = musicians[4]},
                new Track("4#Dior#216#1#"){Musician = musicians[0]},
                new Track("5#See You Again#229#2#"){Musician = musicians[1]},
                new Track("6#Chosen#161#3#"){Musician = musicians[2]},
                new Track("7#HIGHEST IN THE ROOM#175#4#"){Musician = musicians[3]},
            };

            mockMusicianRepo = new Mock<IRepository<Musician>>();
            mockMusicianRepo.Setup(m => m.ReadAll()).Returns(musicians.AsQueryable);

            mockTrackRepo = new Mock<IRepository<Track>>();
            mockTrackRepo.Setup(m => m.ReadAll()).Returns(tracks.AsQueryable);

            mockRecordLabelRepo = new Mock<IRepository<RecordLabel>>();
            mockRecordLabelRepo.Setup(m => m.ReadAll()).Returns(recordLabels.AsQueryable);

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
            Musician newTravis = new Musician("4#Travis Scott#04/29/1991#Budapest#Hungary#male#3");

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

        [Test]
        public void CheckMusiciansFromDeathRowRecords()
        {
            //ARRANGE
            var shouldbe = new List<Musician>
            {
                new Musician("3#Tyga#11/19/1998#Compton#USA#male#3"),
                new Musician("4#Travis Scott#04/30/1991#Houston#USA#male#3"),
                new Musician("5#Russ#09/26/1992#Atlanta#USA#male#3")
            };

            //ACT
            var result = mLogic.MusiciansFromDeathRowRecords();

            //ASSERT
            CollectionAssert.AreEqual(shouldbe, result);
        }

        [Test]
        public void CheckMusicianAverageAgeInTheRecordLabels()
        {
            //ARRANGE

            var shouldbe = new List<AvgAgeInRecordLabel>
            {
                new AvgAgeInRecordLabel(){
                    RecordLabel ="RCA Records",
                    AvgAge=23
                },
                new AvgAgeInRecordLabel(){
                    RecordLabel ="Capitol Records",
                    AvgAge=31
                },
                new AvgAgeInRecordLabel(){
                    RecordLabel ="Death Row Records",
                    AvgAge=(double)85/(double)3
                }

            }.AsQueryable();

            //ACT
            var result = mLogic.MusicianAverageAgeInTheRecordLabels();

            //ASSERT
            CollectionAssert.AreEqual(result, shouldbe);
        }

        [Test]
        public void CheckTracksFromMusicianBornAfter98()
        {
            //ARRANGE
            var shouldbe = new List<Track>
            {
                new Track("4#Dior#216#1#")
            };

            //ACT
           var result = tLogic.TracksFromMusicianBornAfter98();

            //ASSERT
            CollectionAssert.AreEqual(shouldbe, result);
        }

        [Test]
        public void CheckSumOfMusicLengthPerMusician()
        {
            //ARRANGE
            var shouldbe = new List<SumOfMusicLength>
            {
                new SumOfMusicLength(){
                    Name ="Russ",
                    Length=506
                },
                new SumOfMusicLength(){
                    Name ="Pop Smoke",
                    Length=216
                },
                new SumOfMusicLength(){
                    Name ="Charlie Puth",
                    Length=229
                },
                new SumOfMusicLength(){
                    Name ="Tyga",
                    Length=161
                },
                new SumOfMusicLength(){
                    Name ="Travis Scott",
                    Length=175
                }
                

            }.AsQueryable();

            //ACT
            var result = tLogic.SumOfMusicLengthPerMusician();

            //ARRANGE
            CollectionAssert.AreEqual(result, shouldbe);
        }
        [Test]
        public void CheckMusiciansWHoHasLongerSongThan200()
        {
            //ARRAGE
            var shouldbe = new List<Musician>
            {
                new Musician("5#Russ#09/26/1992#Atlanta#USA#male#3"),
                new Musician("1#Pop Smoke#07/20/1999#New York City#USA#male#2"),
                new Musician("2#Charlie Puth#12/02/1991#Rumson#USA#male#1")
                
            }.AsQueryable();

            //ACT
            var result = tLogic.MusiciansWHoHasLongerSongThan200();

            //ASSERT
            CollectionAssert.AreEqual(result, shouldbe);


        }
    }
}
