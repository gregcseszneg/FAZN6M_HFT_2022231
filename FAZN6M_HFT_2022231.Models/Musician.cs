using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAZN6M_HFT_2022231.Models
{
    public class Musician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MuscicianId { get; set; }
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
            Name = split[0];
            DateOfBirth = DateTime.Parse(split[1]);
            HomeTown = split[2];
            Country = split[3];
            Age = int.Parse(split[4]);
            Gender = split[5];
            RecordLabelId= int.Parse(split[6]);
        }
        public virtual RecordLabel RecordLabel { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }

    }
}
