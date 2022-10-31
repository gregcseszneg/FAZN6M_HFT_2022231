using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Models
{
    internal class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateOfRelease { get; set; }

        [ForeignKey(nameof(Musician))]
        public int MusicianId { get; set; }

        [ForeignKey(nameof(Track))]
        public int TrackId { get; set; }
    }
}
