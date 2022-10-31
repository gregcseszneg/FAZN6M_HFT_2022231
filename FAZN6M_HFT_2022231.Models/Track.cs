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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrackId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Length { get; set; } //in sec

        public string Genre { get; set; }

        [Required]
        public int MusicianId { get; set; }

        public int AlbumId { get; set; }

        public Track()
        {

        }

        public Track(string line)
        {
            string[] split = line.Split('#');
            Name = split[0];
            Length = int.Parse(split[1]);
            Genre = split[2];
            MusicianId = int.Parse(split[3]);
            AlbumId = int.Parse(split[4]);
        }
    }
}
