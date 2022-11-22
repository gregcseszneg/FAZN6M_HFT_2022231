using FAZN6M_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace FAZN6M_HFT_2022231.Logic
{
    public interface IAlbumLogic
    {
        void Create(Album item);
        void Delete(int id);
        Album Read(int id);
        IEnumerable<Album> ReadAll();
        void Update(Album item);
    }
}