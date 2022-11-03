using Microsoft.EntityFrameworkCore;
using Castle.DynamicProxy.Contributors;
using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Repository
{
    public partial class MusicDbContext : DbContext
    {
        public DbSet<Musician> musicians { get; set; }
        public DbSet<Album> albums { get; set; }
        public DbSet<Track> tracks { get; set; }
        public DbSet<RecordLabel> recordlabels { get; set; }

        public MusicDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {

                builder
                    .UseInMemoryDatabase("asd")
                    .UseLazyLoadingProxies();
            }
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musician>(musician => musician
            .HasOne(musician => musician.RecordLabel)
            .WithMany(label => label.Musicians)
            .HasForeignKey(musician => musician.RecordLabelId)
            .OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<Album>(album => album
            .HasOne(album => album.Musician)
            .WithMany(musician => musician.Albums)
            .HasForeignKey(album => album.MusicianId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Track>(track => track
            .HasOne(track => track.Musician)
            .WithMany(musicians => musicians.Tracks)
            .HasForeignKey(track => track.MusicianId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<RecordLabel>();
        }

    }
}
