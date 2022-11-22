using FAZN6M_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace FAZN6M_HFT_2022231.Logic
{
    internal interface ITrackLogic
    {
        void Create(Track item);
        void Delete(int id);
        Track Read(int id);
        IEnumerable<Track> ReadAll();
        void Update(Track item);
    }
}