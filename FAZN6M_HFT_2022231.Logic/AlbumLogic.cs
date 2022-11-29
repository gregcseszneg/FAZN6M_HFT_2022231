using FAZN6M_HFT_2022231.Models;
using FAZN6M_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Logic
{
    public class AlbumLogic : IAlbumLogic
    {
        IRepository<Album> repo;
        public AlbumLogic(IRepository<Album> repo)
        {
            this.repo = repo;
        }
        public void Create(Album item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Album Read(int id)
        {
            return this.repo.Read(id);
        }

        public IEnumerable<Album> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Album item)
        {
            this.repo.Update(item);
        }
    }
}
