using FAZN6M_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using FAZN6M_HFT_2022231.Repository;
using System.Linq;
using System.Collections.Generic;

namespace FAZN6M_HFT_2022231.Logic
{

    public class MusicianLogic : IMusicianLogic
    {
        IRepository<Musician> repo;
        public MusicianLogic(IRepository<Musician> repo)
        {
            this.repo = repo;
        }
        public void Create(Musician item)
        {
            if (item.Name=="")
            {
                throw new ArgumentException("The Musican must have a name!");
            }
            if (item.Age>120 || item.Age<1)
            {
                throw new ArgumentException("Given age is invalid!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Musician Read(int id)
        {
            return this.repo.Read(id);
        }

        public IEnumerable<Musician> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Musician item)
        {
            this.repo.Update(item);
        }

        
    }
}
