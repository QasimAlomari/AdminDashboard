using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGlobalService<T>
    {
        Task<IList<T>> GetList(T entity);
        Task<T> GetSpecificRows(T entity);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Active(T entity);
    }
}
