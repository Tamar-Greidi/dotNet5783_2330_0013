using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>  ///interface of Crud functions.
    {
        public int Add(T value);
        public T Get(int value);
        public IEnumerable<T?> GetAll();
        public int Update(T value);
        public void Delete(int value);
    }
}