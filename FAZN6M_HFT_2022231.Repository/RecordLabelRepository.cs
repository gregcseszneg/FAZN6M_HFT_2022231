using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Repository
{
    public class RecordLabelRepository : Repository<RecordLabel>, IRepository<RecordLabel>
    {
        public RecordLabelRepository(MusicDbContext ctx) : base(ctx)
        {
        }

        public override RecordLabel Read(int id)
        {
            return ctx.RecordLabels.FirstOrDefault(x => x.RecordLabelId == id);
        }

        public override void Update(RecordLabel item)
        {
            var old = Read(item.RecordLabelId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
