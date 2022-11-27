using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Models
{
    public class SumOfMusicLength
    {
        public string Name { get; set; }
        public int Length { get; set; }

        public override bool Equals(object obj)
        {
            if (obj==null)
            {
                return false;
            }
            else
            {
                return (obj as SumOfMusicLength).Name == this.Name && (obj as SumOfMusicLength).Length == this.Length;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Length);
        }
    }
}
