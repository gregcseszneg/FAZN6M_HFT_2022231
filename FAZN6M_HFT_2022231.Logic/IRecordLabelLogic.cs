using FAZN6M_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace FAZN6M_HFT_2022231.Logic
{
    public interface IRecordLabelLogic
    {
        void Create(RecordLabel item);
        void Delete(int id);
        RecordLabel Read(int id);
        IEnumerable<RecordLabel> ReadAll();
        void Update(RecordLabel item);
    }
}