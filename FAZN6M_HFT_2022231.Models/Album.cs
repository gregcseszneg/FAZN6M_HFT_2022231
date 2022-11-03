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

        public DateTime YearOfRelease { get; set; }

        [ForeignKey(nameof(Musician))]
        public int MusicianId { get; set; }

        [ForeignKey(nameof(Track))]
        public int TrackId { get; set; }

        public Album()
        {

        }

        public Album(string line)
        {
            string[] split = line.Split('#');
            Name = split[0];
            YearOfRelease = DateTime.Parse(split[1]);
            MusicianId = int.Parse(split[2]);
            TrackId=int.Parse(split[3]);
        }
        public virtual Musician Musician { get; set; }
    }
}
