using FAZN6M_HFT_2022231.Models;
using System.Linq;

namespace FAZN6M_HFT_2022231.Logic
{
    internal interface ITrackLogic
    {
        void Create(Track item);
        void Delete(int id);
        Track Read(int id);
        IQueryable<Track> ReadAll();
        void Update(Track item);
    }
}