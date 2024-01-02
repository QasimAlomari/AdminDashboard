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
    public class CategoryService : ICategoryService<Category>
    {
        private readonly IRepository<Category> _Repository;

        public CategoryService(IRepository<Category> repository)
        {
            _Repository = repository;
        }


        public async Task Active(Category entity)
        {
            var obj = new
            {
                CategoryId = entity.CategoryId,
                IsActive = entity.IsActive,
                UpdateId = entity.UpdateId,
            };

            await _Repository.ExecCommand("Sp_Category_Active", obj);
        }

        public async Task Add(Category entity)
        {
            var obj = new
            {
                CategoryName = entity.CategoryName,
                CreateId = entity.CreateId,
            };

            await _Repository.ExecCommand("Sp_Category_Insert", obj);
        }

        public async Task Delete(Category entity)
        {
            var obj = new
            {
                CategoryId = entity.CategoryId,
                UpdateId = entity.UpdateId,
            };
            await _Repository.ExecCommand("Sp_Category_Delete", obj);
        }

        public async Task<IList<Category>> GetList(Category entity)
        {
            var obj = new
            {

            };

           return await _Repository.ListData("Sp_Category_Select", obj);
        }

        public async Task<Category> GetSpecificRows(Category entity)
        {
            var obj = new
            {
                CategoryId = entity.CategoryId,
            };
            return await _Repository.FindExecCommand("Sp_Category_SelectById", obj);
        }

        public async Task Update(Category entity)
        {
            var obj = new
            {
                CategoryId = entity.CategoryId,
                CategoryName = entity.CategoryName,
                UpdateId = entity.UpdateId,
            };
            await _Repository.ExecCommand("Sp_Category_Update", obj);
        }
    }
}
