using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAZN6M_HFT_2022231.Models
{
    public class Musician
    {
        [Key]
        [Required]
        public int MusicianId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string HomeTown { get; set; }
        public string Country { get; set; }
        [Range(0,120)]
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
            string[] split = line.Split('#');
            MusicianId = int.Parse(split[0]);
            Name = split[1];
            DateOfBirth = DateTime.Parse(split[2]);
            HomeTown = split[3];
            Country = split[4];
            Age = DateTime.Today.Year - DateOfBirth.Year;
            Gender = split[5];
            if (split[6]!="")
            {
                RecordLabelId = int.Parse(split[6]);
            }
        }
        public virtual RecordLabel RecordLabel { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }

    }
}
