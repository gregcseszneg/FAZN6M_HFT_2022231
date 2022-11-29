using ConsoleTools;
using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace FAZN6M_HFT_2022231.Client
{
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            if (entity == "Musician")
            {
                Console.WriteLine("Enter musician name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter musician id:");
                int id=int.Parse(Console.ReadLine());
                Console.WriteLine("Enter record label id:");
                int rlId = int.Parse(Console.ReadLine());
                rest.Post(new Musician()
                {
                    Name = name,
                    MusicianId = id,
                    RecordLabelId = rlId
                }, "musician");
            }
            else if(entity =="Track")
            {
                Console.WriteLine("Enter track name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter track id:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter musician id:");
                int musicianId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter track length");
                int length = int.Parse(Console.ReadLine());
                rest.Post(new Track()
                {
                    Name = name,
                    MusicianId=musicianId,
                    TrackId = id,
                    Length = length
                }, "track");
            }
            else if (entity == "Album")
            {
                Console.WriteLine("Enter album name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter album id:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter musician id:");
                int musicianId = int.Parse(Console.ReadLine());
                rest.Post(new Album()
                {
                    Name = name,
                    AlbumId = id,
                    MusicianId = id
                }, "album"); 
            }
            else if (entity == "Record label")
            {
                Console.WriteLine("Enter record label name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter record label id:");
                int id = int.Parse(Console.ReadLine());

                rest.Post(new RecordLabel()
                {
                    Name = name,
                    RecordLabelId = id
                }, "recordlabel");
            }
        }

        static void Update(string entity)
        {
            if (entity == "Musician")
            {
                Console.Write("Enter Musician's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Musician current = rest.Get<Musician>(id, "musician");
                Console.Write($"New name (the old is {current.Name}): ");
                string name = Console.ReadLine();
                current.Name = name;
                rest.Put(current, "musician");
            }
            else if (entity == "Track")
            {
                Console.Write("Enter track's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Track current = rest.Get<Track>(id, "track");
                Console.Write($"New name (the old is {current.Name}): ");
                string name = Console.ReadLine();
                current.Name = name;
                rest.Put(current, "track");
            }
            else if (entity == "Album")
            {
                Console.Write("Enter album's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Album current = rest.Get<Album>(id, "album");
                Console.Write($"New name (the old is {current.Name}): ");
                string name = Console.ReadLine();
                current.Name = name;
                rest.Put(current, "album");
            }
            else if (entity == "Record label")
            {
                Console.Write("Enter recordlabel's id to update: ");
                int id = int.Parse(Console.ReadLine());
                RecordLabel current = rest.Get<RecordLabel>(id, "recordlabel");
                Console.Write($"New name (the old is {current.Name}): ");
                string name = Console.ReadLine();
                current.Name = name;
                rest.Put(current, "recordlabel");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Musician")
            {
                Console.Write("Enter musician's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "musician");
            }
            else if (entity == "Track")
            {
                Console.Write("Enter track's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "track");
            }
            else if (entity == "Album")
            {
                Console.Write("Enter album's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "album");
            }
            else if (entity == "Record label")
            {
                Console.Write("Enter record label's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "recordlabel");
            }
        }

        static void List(string entity)
        {
            if (entity == "Musician")
            {
                List<Musician> musicians = rest.Get<Musician>("musician");
                foreach (var item in musicians)
                {
                    Console.WriteLine(item.MusicianId + " " + item.Name);
                }
                Console.ReadLine();
            }
            else if(entity == "Track")
            {
                List<Track> tracks = rest.Get<Track>("track");
                foreach (var item in tracks)
                {
                    Console.WriteLine(item.TrackId + " " + item.Name);
                }
                Console.ReadLine();
            }
            else if (entity == "Album")
            {
                List<Album> albums = rest.Get<Album>("album");
                foreach (var item in albums)
                {
                    Console.WriteLine(item.AlbumId + " " + item.Name);
                }
                Console.ReadLine();
            }
            else if (entity == "Record label")
            {
                List<RecordLabel> recordLabels = rest.Get<RecordLabel>("recordlabel");
                foreach (var item in recordLabels)
                {
                    Console.WriteLine(item.RecordLabelId +" "+ item.Name);
                }
                Console.ReadLine();
            }
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:34694/", "musician");

            var musicianSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Musician"))
                .Add("Create", () => Create("Musician"))
                .Add("Delete", () => Delete("Musician"))
                .Add("Update", () => Update("Musician"))
                .Add("Exit", ConsoleMenu.Close);

            var trackSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Track"))
                .Add("Create", () => Create("Track"))
                .Add("Delete", () => Delete("Track"))
                .Add("Update", () => Update("Track"))
                .Add("Exit", ConsoleMenu.Close);

            var albumSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Album"))
                .Add("Create", () => Create("Album"))
                .Add("Delete", () => Delete("Album"))
                .Add("Update", () => Update("Album"))
                .Add("Exit", ConsoleMenu.Close);

            var recordlabelSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Record label"))
                .Add("Create", () => Create("Record label"))
                .Add("Delete", () => Delete("Record label"))
                .Add("Update", () => Update("Record label"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Musicians", () => musicianSubMenu.Show())
                .Add("Tracks", () => trackSubMenu.Show())
                .Add("Albums", () => albumSubMenu.Show())
                .Add("Record labels", () => recordlabelSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
