using Domain.AutoMapper;
using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.UnitWork;
using WebApi.Authorization;
using static Domain.Common.CommonClass;

namespace WebApi.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]  // UnAuthorize and Authorize

    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }


        [HttpPost(nameof(GetList))]
        public async Task<ResponseStandardJsonApi> GetList()
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var ObjList = await _UnitOfWork.Product.GetList(new Product());
                var Mapper = AutoMapperConfiguration.CreateMapper();

                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);
                var NewData = Mapper.Map<IList<ProductModel>>(ObjList);
                if (NewData.Count > 0)
                {
                    var CategoryList = await _UnitOfWork.Category.GetList(new Category());
                    var NewCategoryList = Mapper.Map<IList<CategoryModel>>(CategoryList);
                    foreach (var obj in NewData)
                    {
                        obj.Category = NewCategoryList.FirstOrDefault(e => e.CategoryId == obj.CategoryId);
                    }
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



        // public async Task<ResponseStandardJson<CategoryModel>> GetById(CategoryModelId Category)
        [HttpPost(nameof(GetById))]
        public async Task<ResponseStandardJson<ProductModel>> GetById(ProductModelId Product)
        {
            var apiResponse = new ResponseStandardJson<ProductModel>();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Product>(Product);
                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);
                var Obj = await _UnitOfWork.Product.GetSpecificRows(Data);
                var NewData = Mapper.Map<ProductModel>(Obj);
                if (NewData != null)
                {
                    var CategoryObj = await _UnitOfWork.Category.GetSpecificRows(new Category()
                    {
                        CategoryId = NewData.CategoryId,
                    });
                    var NewCategoryObj = Mapper.Map<CategoryModel>(CategoryObj);
                    NewData.Category = NewCategoryObj;
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
                    apiResponse.Result = new ProductModel();
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new ProductModel();
            }
            return apiResponse;
        }

        [HttpPost(nameof(Add))]
        public async Task<ResponseStandardJsonApi> Add(ProductModelCreate Product)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Product>(Product);
                Data.CreateId = Data.CreateId;
                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);
                await _UnitOfWork.Product.Add(Data);
                apiResponse.Message = "Add Successfully";
                apiResponse.Code = Ok().StatusCode;
                apiResponse.Success = true;
                apiResponse.Result = new NullColumns[] { };
            }
            catch(Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new NullColumns[] { };
            }
            return apiResponse;
        }


        [HttpPost(nameof(Update))]
        public async Task<ResponseStandardJsonApi> Update(ProductModelUpdate Product)  // Update([FromForm]ProductModelUpdate Product) ==> using in ajax
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Product>(Product);
                Data.UpdateId = Data.UpdateId;
                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);
                await _UnitOfWork.Product.Update(Data);
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
        public async Task<ResponseStandardJsonApi> Delete(ProductModelId Product)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Product>(Product);
                Data.UpdateId = Data.UpdateId;
                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);
                await _UnitOfWork.Product.Delete(Data);
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
        public async Task<ResponseStandardJsonApi> Active(ProductModelId Product)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var Mapper = AutoMapperConfiguration.CreateMapper();
                var Data = Mapper.Map<Product>(Product);
                var token = Request.Headers["Authorization"].ToString();
                var UserInfo = InformationToken.GetInfoUsers(token);
                var Obj = await _UnitOfWork.Product.GetSpecificRows(Data);
                if (Obj != null)
                {
                    Obj.IsActive = !Obj.IsActive;
                    Obj.UpdateId = Obj.UpdateId;
                    await _UnitOfWork.Product.Active(Obj);
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
