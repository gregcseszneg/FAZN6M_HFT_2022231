using System;
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

    }
}
