using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAZN6M_HFT_2022231.Repository
{
    internal interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
