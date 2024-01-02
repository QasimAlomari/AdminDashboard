using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.IStore
{
    public interface IUserService<User> : IGlobalService<User>
    {
        Task<User> GetUserNamePassword(User entity);
    }
}
