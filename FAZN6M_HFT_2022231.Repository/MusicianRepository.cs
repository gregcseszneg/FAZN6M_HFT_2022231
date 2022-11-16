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
            return ctx.Musicians.FirstOrDefault(x => x.MusicianId == id);
        }

        public override void Update(Musician item)
        {
            var old=Read(item.MusicianId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
