using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IRepository <T>
    {
        Task<IList<T>> ListData(string SpName, object Params);
        Task ExecCommand(string SpName, object Params);
        Task<T> FindExecCommand(string SpName, object Params);
    }
}
