using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using FAZN6M_HFT_2022231.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FAZN6M_HFT_2022231.Test
{
    public class FakeMusicianRepositry : IRepository<Musician>
    {
        public void Create(Musician item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Musician Read(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Musician> ReadAll()
        {
            return new List<Musician>()
            {
                new Musician("1#Pop Smoke#07/20/1999#New York City#USA#male#2"),
                new Musician("2#Charlie Puth#12/02/1991#Rumson#USA#male#1"),
                new Musician("3#Tyga#11/19/19898#Compton#USA#male#3"),
                new Musician("4#Travis Scott#04/30/1991#Houston#USA#male#3"),
                new Musician("5#Joyner Lucas#08/17/1988#Worcester#USA#male#")

            }.AsQueryable();
        }

        public void Update(Musician item)
        {
            throw new NotImplementedException();
        }
    }
    [TestFixture]
    public class MusicianTester
    {
        MusicianLogic logic;
        [SetUp]
        public void InIt()
        {
            
           logic  = new MusicianLogic(new FakeMusicianRepositry());
        }
        [Test]
        public void CheckCreate()
        {

        }

        [Test]
        public void CheckCreateNull()
        {

        }
        [Test]
        public void CheckDelete()
        {

        }
        [Test]
        public void CheckUpdate()
        {

        }
        [Test]
        public void CheckRead()
        {

        }
        [Test]
        public void CheckReadAll()
        {

        }
    }
}
