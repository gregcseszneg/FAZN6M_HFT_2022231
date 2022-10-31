using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Models
{
    internal class Track
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
    }
}
