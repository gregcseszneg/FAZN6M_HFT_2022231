﻿using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Repository
{
    public class AlbumRepository : Repository<Album>, IRepository<Album>
    {
        public AlbumRepository(MusicDbContext ctx) : base(ctx)
        {
        }

        public override Album Read(int id)
        {
            return ctx.Albums.FirstOrDefault(x => x.AlbumId == id);
        }

        public override void Update(Album item)
        {
            var old = Read(item.AlbumId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual)==null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
