using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Models
{
    public class AvgAgeInRecordLabel
    {
        public string RecordLabel { get; set; }
        public double AvgAge { get; set; }

        public override bool Equals(object obj)
        {

                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return (obj as AvgAgeInRecordLabel).RecordLabel == this.RecordLabel && (obj as AvgAgeInRecordLabel).AvgAge == this.AvgAge;
                };

            
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(RecordLabel, AvgAge);
        }
        public override string ToString()
        {
            return RecordLabel + " " + AvgAge;
        }
    }
}
