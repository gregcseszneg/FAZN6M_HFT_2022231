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
    public class RecordLabel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordLabelId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime YearOfFoundation { get; set; }

        public string Country { get; set; }
        public string Headquarters { get; set; }

        public RecordLabel()
        {

        }

        public RecordLabel(string line)
        {
            string[] split = line.Split('#');
            Name = split[0];
            YearOfFoundation = DateTime.Parse(split[1]);
            Country = split[2];
            Headquarters = split[3];
        }

        public virtual ICollection<Musician> Musicians { get; set; }
    }
}