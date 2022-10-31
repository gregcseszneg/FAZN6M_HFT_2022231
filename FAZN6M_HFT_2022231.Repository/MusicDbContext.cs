using Arch.EntityFrameworkCore;
using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Repository
{
    internal class MusicDbContext : DbContext
    {
        public DbSet<Musician> musicians { get; set; }
        public DbSet<Album> albums { get; set; }
        public DbSet<Track> tracks { get; set; }
        public DbSet<RecordLabel> recordlabels { get; set; }

        
    }
}
