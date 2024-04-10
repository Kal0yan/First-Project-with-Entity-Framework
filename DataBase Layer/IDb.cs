using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase_Layer
{
    public interface IDb<T, K>
    {
        void Create(T entity);

        T Read(K key, bool isReadOnly = true, bool useNavigationalProperties = false);

        List<T> ReadAll(bool isReadOnly = true, bool useNavigationalProperties = false);

        void Update(T entity, bool useNavigationalProperties = false);

        void Delete(K key);
    }
}
