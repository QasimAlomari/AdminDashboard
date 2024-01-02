using Domain.Entities;
using Repository.Repositories;
using Services.Interfaces;
using Services.Interfaces.IStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Store
{
    public class ProductService : IProductService<Product>
    {
        public IRepository<Product> _Repository { get; }

        public ProductService(IRepository<Product> repository)
        {
            _Repository = repository;
        }


        public async Task Active(Product entity)
        {
            var obj = new
            {
                ProductId = entity.ProductId,
                IsActive = entity.IsActive,
                UpdateId = entity.UpdateId,
            };
            await _Repository.ExecCommand("Sp_Product_Active", obj);
        }

        public async Task Add(Product entity)
        {
            var obj = new
            {
                ProductName = entity.ProductName,
                ProductPrice = entity.ProductPrice,
                CategoryId = entity.CategoryId,
                CreateId = entity.CreateId,
            };
            await _Repository.ExecCommand("Sp_Product_Insert", obj);
        }

        public async Task Delete(Product entity)
        {
            var obj = new
            {
                ProductId = entity.ProductId,
                UpdateId = entity.UpdateId,
            };
            await _Repository.ExecCommand("Sp_Product_Delete", obj);
        }

        public async Task<IList<Product>> GetList(Product entity)
        {
            var obj = new
            {
            };
            return await _Repository.ListData("Sp_Product_Select", obj);
        }

        public async Task<Product> GetSpecificRows(Product entity)
        {
            var obj = new
            {
                ProductId = entity.ProductId,
            };
            return await _Repository.FindExecCommand("Sp_Product_SelectById", obj);
        }

        public async Task Update(Product entity)
        {
            var obj = new
            {
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                ProductPrice = entity.ProductPrice,
                CategoryId = entity.CategoryId,
                UpdateId = entity.UpdateId,
            };
            await _Repository.ExecCommand("Sp_Product_Update", obj);
        }
    }
}
