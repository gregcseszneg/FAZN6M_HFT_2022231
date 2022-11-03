using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Models
{
    public class Track
    {
        [Key]
        [Required]
        public int TrackId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Length { get; set; } //in sec

        [Required]
        [ForeignKey(nameof(Musician))]
        public int MusicianId { get; set; }
        public int AlbumId { get; set; }

        public Track()
        {

        }

        public Track(string line)
        {
            string[] split = line.Split('#');
            TrackId = int.Parse(split[0]);
            Name = split[1];
            Length = int.Parse(split[2]);
            MusicianId = int.Parse(split[3]);
            AlbumId = int.Parse(split[4]);
        }

        public virtual Musician Musician { get; set; }
    }
}
