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
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<RecordLabel> RecordLabels{ get; set; }

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

            modelBuilder.Entity<RecordLabel>().HasData(new RecordLabel[]
            {
                new RecordLabel("1#Capitol Records#1942#USA#Hollywood"),
                new RecordLabel("2#RCA Records#1900#USA#New York City"),
                new RecordLabel("3#Death Row Records#1991#USA#Beverly Hills"),
                new RecordLabel("4#Bad Boy Entertainment#1993#USA#New York City"),
                new RecordLabel("5#Atlantic Records#1947#USA#New York City"),
                new RecordLabel("6#Young Money Entertainment#2005#USA#New Orleans"),
                new RecordLabel("7#Warner Music Group#1958#USA#New York City")

            });

            modelBuilder.Entity<Musician>().HasData(new Musician[]
            {
                new Musician("1#G-eazy#05/24/1989#Oakland#USA#male#2"),
                new Musician("2#NF#03/30/1991#Gladwin#USA#male#1"),
                new Musician("3#Dr. Dre#02/18/1965#Compton#USA#male#3"),
                new Musician("4#Snoop Dogg10/20/1971#Long Beach#USA#male#3"),
                new Musician("5#Russ#09/26/1992#Atlanta#USA#male#"),
                new Musician("6#Machine Gun Kelly#04/22/1990#Oakand#USA#male#4"),
                new Musician("7#Ed Sheeran#02/17/1991#Hebden Bridge#England#male#5"),
                new Musician("8#T. Danny#10/25/1998#Budapest#Hungary#male#"),
                new Musician("9#Drake#10/24/1986#Toronto#Canada#male#6"),
                new Musician("10#Nicki Minaj#12/08/1982#Port of Spain#Trinidad#female#6"),
                new Musician("11#Central Cee#06/04/1998#London#England#male#7"),
                new Musician("12#David Guetta#11/07/1967#Paris#France#7"),
                new Musician("13#Bruno Mars#10/08/1985#Honolulu#Hawaii#male#5"),
                new Musician("14#Chris Brown#05/05/1989#Tappahancock#USA#male#2")
            });

            modelBuilder.Entity<Track>().HasData(new Album[]
            {
                new Album("1#SHAKE THE SNOW GLOBE#2020#5#14"),
                new Album("2#These Things Happen#2014#1#16"),
                new Album("3#The Search#2019#2#20"),
                new Album("4#2001#1999#3#23"),
                new Album("5#R&G (Rhythm & Gangsta): The Mesterpiece#2004#4#13"),
                new Album("6#Tickets To My Downfall#2020#6#15"),
                new Album("7#Certificied Lover Boy#2021#9#21"),
                new Album("8#23#2022#11#15"),
                new Album("9#Nothing but the Beat#2012#12#29"),
                new Album("10#24K Magic#2016#13#9"),
                new Album("11#Indigo#2019#14#32")
            }); ;

            modelBuilder.Entity<Track>().HasData(new Track[]
            {
                new Track("1#Are You Entertained#156#5#"),
                new Track("2#CIVIL WAR#144#5#1"),
                new Track("3#GUESS WHAT#206#5#1"),
                new Track("4#These Things Happen#144#1#2"),
                new Track("5#I Mean It#236#1#2"),
                new Track("6#Almost Famous#268#1#2"),
                new Track("7#Lotta That#288#1#2"),
                new Track("8#Downtown Love#326#1#2"),
                new Track("9#Let's Get Lost#240#1#2"),
                new Track("10#Shoot Me Down#196#1#2"),
                new Track("11#Been On#208#1#2"),
                new Track("12#Remember You#215#1#2"),
                new Track("13#Tumblr Girls#255#1#2"),
                new Track("14#Leave Me Alone#308#2#3"),
                new Track("15#My Stress#252#2#3"),
                new Track("16#Only#225#2#3"),
                new Track("17#When I Grow Up#196#2#3"),
                new Track("18#Time#240#2#3"),
                new Track("19#Still D.R.E#270#3#4"),
                new Track("20#What's The Difference#244#3#4"),
                new Track("21#Uh#179#8#"),
                new Track("22#Signs#236#4#5"),
                new Track("23#Bloody Valentine#205#6#6"),
                new Track("24#Lonely#190#6#6"),
                new Track("25#Fair Trade#291#9#7"),
                new Track("26#Could Shoulder#192#11#8"),
                new Track("27#Khabib#201#11#8"),
                new Track("28#Titanium#245#12#9"),
                new Track("29#That's What I like#206#13#10"),
                new Track("30#No Guidance#260#14#11")
            });
        }

    }
}
