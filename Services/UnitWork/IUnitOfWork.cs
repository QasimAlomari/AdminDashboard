using Domain.Entities;
using Services.Interfaces.IStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UnitWork
{
    public interface IUnitOfWork
    {
        public ICategoryService<Category> Category { get; }
        public IProductService<Product> Product { get; }
        public IUserService<User> User { get; }
    }
}
