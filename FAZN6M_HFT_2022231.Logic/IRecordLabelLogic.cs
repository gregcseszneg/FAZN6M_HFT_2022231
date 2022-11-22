using FAZN6M_HFT_2022231.Models;
using System.Linq;

namespace FAZN6M_HFT_2022231.Logic
{
    internal interface IRecordLabelLogic
    {
        void Create(RecordLabel item);
        void Delete(int id);
        RecordLabel Read(int id);
        IQueryable<RecordLabel> ReadAll();
        void Update(RecordLabel item);
    }
}