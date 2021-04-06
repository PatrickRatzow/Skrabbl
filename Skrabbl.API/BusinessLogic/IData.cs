using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.BusinessLogicLayer
{
    public interface IData<T>
    {
        T Get(int id);
        List<T> Get();
        int Add(T toAdd);
        bool Put(T toUpdate);
        bool Delete(int id);
    }
}
