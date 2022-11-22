using FAZN6M_HFT_2022231.Models;
using System.Linq;

namespace FAZN6M_HFT_2022231.Logic
{
    public interface IMusicianLogic
    {
        void Create(Musician item);
        void Delete(int id);
        Musician Read(int id);
        IQueryable<Musician> ReadAll();
        void Update(Musician item);
    }
}