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
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordLabelId { get; set; }

        [Required]
        public string Name { get; set; }

        public int YearOfFoundation { get; set; }

        public string Country { get; set; }
        public string Headquarters { get; set; }

        public RecordLabel()
        {
            Musicians = new HashSet<Musician>();
        }

        public RecordLabel(string line)
        {
            Musicians = new HashSet<Musician>();
            string[] split = line.Split('#');
            RecordLabelId = int.Parse(split[0]);
            Name = split[1];
            YearOfFoundation = int.Parse(split[2]);
            Country = split[3];
            Headquarters = split[4];
        }

        public virtual ICollection<Musician> Musicians { get; set; }

        public override bool Equals(object obj)
        {
            RecordLabel rl=obj as RecordLabel;
            if (rl == null) return false;
            else
            {
                return rl.RecordLabelId.Equals(this.RecordLabelId)
                    && rl.Name.Equals(this.Name)
                    && rl.YearOfFoundation.Equals(this.YearOfFoundation)
                    && rl.Country.Equals(this.Country)
                    && rl.Headquarters.Equals(this.Headquarters);
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(RecordLabelId,Name, YearOfFoundation, Country, Headquarters);
        }
        public override string ToString()
        {
            return RecordLabelId + " " + Name;
        }
    }
}