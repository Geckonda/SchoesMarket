using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesMarket.Domain.Abstractions
{
    public interface IBaseRepository <T>
    {
        T? GetOneById(int id);
        List<T>? GetAll();
        void Update(T entity);
        void Delete(T entity);
        void Add (T entity);
    }
}
