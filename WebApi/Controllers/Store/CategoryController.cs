using Domain.AutoMapper;
using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Services.UnitWork;
using WebApi.Authorization;
using static Domain.Common.CommonClass;

namespace WebApi.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]  // UnAuthorize and Authorize
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }


        [HttpPost(nameof(GetList))]
        //[HttpPost(nameof(GetList)),AllowAnonymous]  // this response without token
        //[HttpPost("GetList")]
        public async Task<ResponseStandardJsonApi> GetList()
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var ObjList = await _UnitOfWork.Category.GetList(new Category());
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var NewData = Mapper.Map<IList<CategoryModel>>(ObjList);
                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);
                if (NewData.Count > 0)
                {
                    apiResponse.Message = "Show Rows";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = true;
                    apiResponse.Result = NewData;
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "No Data";
                    apiResponse.Code = NotFound().StatusCode;
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


        [HttpPost(nameof(GetById))]
        //[HttpPost(nameof(GetById)), AllowAnonymous]
        public async Task<ResponseStandardJson<CategoryModel>> GetById(CategoryModelId Category)
        {
            var apiResponse = new ResponseStandardJson<CategoryModel>();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Category>(Category);
                var Obj = await _UnitOfWork.Category.GetSpecificRows(Data);
                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);
                var NewData = Mapper.Map<CategoryModel>(Obj);
                if (NewData != null)
                {
                    apiResponse.Message = "Show Row";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = true;
                    apiResponse.Result = NewData;
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "No Data";
                    apiResponse.Code = NotFound().StatusCode;
                    apiResponse.Result = new CategoryModel();
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new CategoryModel();
            }
            return apiResponse;
        }

        [HttpPost(nameof(Add))]
        public async Task<ResponseStandardJsonApi> Add(CategoryModelCreate Category)
        {
            
            var token = Request.Headers["Authorization"].ToString();
            var UserInfo = InformationToken.GetInfoUsers(token);

            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Category>(Category);
                //Data.CreateId = "1";
                //Data.UpdateId = UserInfo.nameid;
                Data.CreateId = UserInfo.nameid;
                await _UnitOfWork.Category.Add(Data);
                apiResponse.Message = "Add Successfully";
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

        [HttpPost(nameof(Update))]
        public async Task<ResponseStandardJsonApi> Update(CategoryModel Category)
        {
            var apiResponse = new ResponseStandardJsonApi();
            var token = Request.Headers["Authorization"].ToString();
            var UserInfo = InformationToken.GetInfoUsers(token);
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Category>(Category);
                //Data.UpdateId = "1";
                Data.UpdateId = UserInfo.nameid;
                await _UnitOfWork.Category.Update(Data);
                apiResponse.Message = "Update Successfully";
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

        [HttpPost(nameof(Delete))]
        public async Task<ResponseStandardJsonApi> Delete(CategoryModelId Category)
        {
            var apiResponse = new ResponseStandardJsonApi();
            var token = Request.Headers["Authorization"].ToString();
            var UserInfo = InformationToken.GetInfoUsers(token);
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Category>(Category);
                //Data.UpdateId = "1";
                Data.UpdateId = UserInfo.nameid;
                await _UnitOfWork.Category.Delete(Data);
                apiResponse.Message = "Delete Successfully";
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

        [HttpPost(nameof(Active))]
        
        public async Task<ResponseStandardJsonApi> Active(CategoryModelId Category)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Category>(Category);

                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);

                var Obj = await _UnitOfWork.Category.GetSpecificRows(Data);
                if (Obj != null)
                {
                    Obj.IsActive = !Obj.IsActive;
                    Obj.UpdateId = UserInfo.nameid;
                    await _UnitOfWork.Category.Active(Obj);
                    apiResponse.Message = "Active / InActive Successfully";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = true;
                    apiResponse.Result = new NullColumns[] { };
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "No Data";
                    apiResponse.Code = NotFound().StatusCode;
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


    }
}
