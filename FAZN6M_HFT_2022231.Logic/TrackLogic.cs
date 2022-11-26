using FAZN6M_HFT_2022231.Models;
using FAZN6M_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Logic
{
    public class TrackLogic : ITrackLogic
    {
        IRepository<Track> repo;
        public TrackLogic(IRepository<Track> repo)
        {
            this.repo = repo;
        }

        public void Create(Track item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Track Read(int id)
        {
            return this.repo.Read(id);
        }

        public IEnumerable<Track> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Track item)
        {
            this.repo.Update(item);
        }
    }
}
