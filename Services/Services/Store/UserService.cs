using Domain.Entities;
using Repository.Repositories;
using Services.Interfaces.IStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Store
{
    public class UserService : IUserService<User>
    {
        private readonly IRepository<User> _Repository;

        public UserService(IRepository<User> repository)
        {
            _Repository = repository;
        }

        public async Task Active(User entity)
        {
            var obj = new
            {
                UserId = entity.UserId,
                IsActive = entity.IsActive,
                UpdateId = entity.UpdateId,
            };
            await _Repository.ExecCommand("Sp_User_Active", obj);
        }

        public async Task Add(User entity)
        {
            var obj = new
            {
                UserName = entity.UserName,
                Email = entity.Email,
                FullName = entity.FullName,
                Password = entity.Password,
                CreateId = entity.CreateId,
            };
            await _Repository.ExecCommand("Sp_User_Insert", obj);
        }

        public async Task Delete(User entity)
        {
            var obj = new
            {
                UserId = entity.UserId,
                UpdateId = entity.UpdateId,
            };
            await _Repository.ExecCommand("Sp_User_Delete", obj);
        }

        public async Task<IList<User>> GetList(User entity)
        {
            var obj = new
            {
            };
            return await _Repository.ListData("Sp_User_Select", obj);
        }

        public async Task<User> GetSpecificRows(User entity)
        {
            var obj = new
            {
                UserId = entity.UserId,
            };
            return await _Repository.FindExecCommand("Sp_User_SelectById", obj);
        }

        public async Task<User> GetUserNamePassword(User entity)
        {
            var obj = new
            {
                UserName = entity.UserName,
                Password = entity.Password,
            };
            return await _Repository.FindExecCommand("Sp_User_Login", obj);
        }

        public async Task Update(User entity)
        {
            var obj = new
            {
                UserName = entity.UserName,
                Email = entity.Email,
                FullName = entity.FullName,
                Password = entity.Password,
                UpdateId = entity.UpdateId,
            };
            await _Repository.ExecCommand("Sp_User_Update", obj);
        }
    }
}
