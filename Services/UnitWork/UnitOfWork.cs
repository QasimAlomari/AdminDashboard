using Domain.Entities;
using Services.Interfaces.IStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryService<Category> Category { get; }

        public IProductService<Product> Product { get; }
        public IUserService<User> User { get; }

        public UnitOfWork(
             ICategoryService<Category> _Category,
             IProductService<Product> _Product,
             IUserService<User> _User
             )
        {
            Category = _Category;
            Product = _Product;
            User = _User;
            
        }
    }
}
