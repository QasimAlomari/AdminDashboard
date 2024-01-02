using Domain.AutoMapper;
using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.UnitWork;
using static Domain.Common.CommonClass;
using WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IAuthentication<UserModel> _Jwt;

        public UserController(IUnitOfWork unitOfWork, IAuthentication<UserModel> Jwt)
        {
            _UnitOfWork = unitOfWork;
            _Jwt = Jwt;
        }

        [HttpPost(nameof(Login))]
        public async Task<ResponseStandardJsonApi> Login(UserLoginModel userLogin)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var data = Mapper.Map<User>(userLogin);
                var User = await _UnitOfWork.User.GetUserNamePassword(data);
                if (User != null)
                {
                    var UserModel = Mapper.Map<UserModel>(User);
                    UserModel.Token = _Jwt.GetJsonWebToken(UserModel);
                    apiResponse.Message = "Login Successfully";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = true;
                    apiResponse.Result = UserModel;
                }
                else
                {
                    apiResponse.Message = "Login Failed";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = false;
                    apiResponse.Result = new NullColumns[] { };
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new NullColumns[] { };
            }
            return apiResponse;
        }

        [HttpPost(nameof(Register))]
        public async Task<ResponseStandardJsonApi> Register(UserRegisterModel UserRegister)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<User>(UserRegister);
                await _UnitOfWork.User.Add(Data);
                apiResponse.Message = "Register Successfully";
                apiResponse.Code = Ok().StatusCode;
                apiResponse.Success = true;
                apiResponse.Result = new NullColumns[] { };
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new NullColumns[] { };
            }
            return apiResponse;
        }
    }
}
