﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FAZN6M_HFT_2022231.Models
{
    public class Musician
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MusicianId { get; set; }
        [Required]
        public string Name { get; set; }
       
        public DateTime DateOfBirth { get; set; }
        public string HomeTown { get; set; }
        public string Country { get; set; }

        [Required]
        [Range(1,120)]
        public int Age { get; set; }
        public string Gender { get; set; }
        [ForeignKey(nameof(RecordLabel))]
        public int RecordLabelId { get; set; }

        public Musician()
        {
            Albums = new HashSet<Album>();
            Tracks = new HashSet<Track>();
        }

        public Musician(string line)
        {
            Albums = new HashSet<Album>();
            Tracks = new HashSet<Track>();
            string[] split = line.Split('#');
            MusicianId = int.Parse(split[0]);
            Name = split[1];
            DateOfBirth = DateTime.Parse(split[2]);
            HomeTown = split[3];
            Country = split[4];
            Age = DateTime.Today.Year - DateOfBirth.Year;
            Gender = split[5];
            RecordLabelId = int.Parse(split[6]);

        }
        [NotMapped]
        [JsonIgnore]
        public virtual RecordLabel RecordLabel { get; set; }
        [JsonIgnore]
        public virtual ICollection<Album> Albums { get; set; }
        [JsonIgnore]
        public virtual ICollection<Track> Tracks { get; set; }

        public override bool Equals(object obj)
        {
            Musician musician = obj as Musician;
            if (musician == null)
            {
                return false;
            }
            return musician.DateOfBirth==this.DateOfBirth && musician.Name == this.Name && musician.MusicianId == this.MusicianId;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(MusicianId, Name, DateOfBirth);
        }
        public override string ToString()
        {
            return MusicianId+" "+Name + " " + Age;
        }

    }
}
