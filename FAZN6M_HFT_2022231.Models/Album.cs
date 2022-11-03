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
    public class Album
    {
        [Key]
        [Required]
        public int AlbumId { get; set; }

        [Required]
        public string Name { get; set; }

        public int YearOfRelease { get; set; }

        [ForeignKey(nameof(Musician))]
        public int MusicianId { get; set; }

        public int NumberOfTracks { get; set; }

        public Album()
        {

        }

        public Album(string line)
        {
            string[] split = line.Split('#');
            AlbumId = int.Parse(split[0]);
            Name = split[1];
            YearOfRelease = int.Parse(split[2]);
            MusicianId = int.Parse(split[3]);
            NumberOfTracks=int.Parse(split[4]);
        }
        public virtual Musician Musician { get; set; }
    }
}
