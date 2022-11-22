using FAZN6M_HFT_2022231.Models;
using FAZN6M_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Logic
{
    internal class RecordLabelLogic
    {
        IRepository<RecordLabel> repository;
        public RecordLabelLogic(IRepository<RecordLabel> repository)
        {
            this.repository = repository;
        }

        public void Create(RecordLabel item)
        {
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public RecordLabel Read(int id)
        {
            return this.repository.Read(id);
        }

        public IQueryable<RecordLabel> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(RecordLabel item)
        {
            this.repository.Update(item);
        }
    }
}
