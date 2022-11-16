using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Repository
{
    internal class MusicianRepository : Repository<Musician>, IRepository<Musician>
    {
        public MusicianRepository(MusicDbContext ctx) : base(ctx)
        {
        }

        public override Musician Read(int id)
        {
           
        }

        public override void Update(Musician item)
        {
            
        }
    }
}
