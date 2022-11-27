using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Repository
{
    public class TrackRepository : Repository<Track>, IRepository<Track>
    {
        public TrackRepository(MusicDbContext ctx) : base(ctx)
        {
        }

        public override Track Read(int id)
        {
            return ctx.Tracks.FirstOrDefault(x => x.TrackId == id);
        }

        public override void Update(Track item)
        {
            var old = Read(item.TrackId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
