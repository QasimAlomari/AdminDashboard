using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Domain.Common.CommonClass;
using WebSite.Helpers;
using System.Text;
using System.Net.Http.Json;
using System.Net;
using NuGet.Protocol;
using System.Net.Http;
using System.Diagnostics;
using Domain.Entities;
using System.Net.Mime;

namespace WebSite.Controllers
{
    public class CategoryController : Controller
    {


        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var stringContent = new StringContent("", Encoding.UTF8, "application/json"); // from server to server


                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var postTask = await client.PostAsJsonAsync("api/Category/GetList", stringContent);

                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<List<CategoryModel>> ResultData = (ResponseStandardJson<List<CategoryModel>>)
                                                                               JsonConvert.DeserializeObject(result.ToString(),
                                                                               (typeof(ResponseStandardJson<List<CategoryModel>>)));

                        return View(ResultData.Result);
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }
            }

            return View();
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int id) 
        {

            using (var client = new HttpClient())
            {
                try
                {
                    ResponseStandardJson<UserModel> t = (ResponseStandardJson<UserModel>)
                                                        JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(), 
                                                        (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", t.Result.Token);

                    var data = new CategoryModelId()
                    {
                        CategoryId = id
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<CategoryModelId>("api/Category/GetById", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<CategoryModel> ResultData = (ResponseStandardJson<CategoryModel>)
                                                                          JsonConvert.DeserializeObject(result.ToString(), 
                                                                          (typeof(ResponseStandardJson<CategoryModel>)));

                        return View(ResultData.Result);

                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }
            }
            
            return View();
        }

        // GET: CategoryController/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModelCreate category)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var data = new CategoryModelCreate()
                    {
                        CategoryName = category.CategoryName,
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<CategoryModelCreate>("api/Category/Add", data);


                    if (postTask.IsSuccessStatusCode)
                    {

                        TempData["SuccessMessage"] = "Category Name Created";
                        return RedirectToAction("Index", "Category");
                    }

                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }
            }

            return View();
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            using(var client = new HttpClient())
            {

                try
                {

                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var data = new CategoryModelId()
                    {
                        CategoryId = id
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<CategoryModelId>("api/Category/GetById", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<CategoryModel> ResultData = (ResponseStandardJson<CategoryModel>)
                                                                          JsonConvert.DeserializeObject(result.ToString(),
                                                                          (typeof(ResponseStandardJson<CategoryModel>)));

                        return View(ResultData.Result);

                    }

                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }

            }
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoryModel category)
        {

            using(var client = new HttpClient())
            {

                try
                {

                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var data = new CategoryModel()
                    {
                        CategoryId = id,
                        CategoryName = category.CategoryName,
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<CategoryModel>("api/Category/Update", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                       
                        return RedirectToAction("Index", "Category");

                    }

                }
                catch (Exception ex)
                {

                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }


            }


            return View();
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            using (var client = new HttpClient())
            {

                try
                {

                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var data = new CategoryModelId()
                    {
                        CategoryId = id
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<CategoryModelId>("api/Category/GetById", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<CategoryModel> ResultData = (ResponseStandardJson<CategoryModel>)
                                                                          JsonConvert.DeserializeObject(result.ToString(),
                                                                          (typeof(ResponseStandardJson<CategoryModel>)));

                        return View(ResultData.Result);

                    }



                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }

            }
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, CategoryModelId category)
        {
            using (var client = new HttpClient())
            {

                try
                {

                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var data = new CategoryModelId()
                    {
                        CategoryId = id, 
                       
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<CategoryModelId>("api/Category/Delete", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;

                        return RedirectToAction("Index", "Category");

                    }

                }
                catch (Exception ex)
                {

                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }


            }


            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Active(int id)
        {

            using (var client = new HttpClient())
            {

                try
                {

                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var data = new CategoryModelId()
                    {
                        CategoryId = id
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<CategoryModelId>("api/Category/GetById", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<CategoryModel> ResultData = (ResponseStandardJson<CategoryModel>)
                                                                          JsonConvert.DeserializeObject(result.ToString(),
                                                                          (typeof(ResponseStandardJson<CategoryModel>)));

                        return View(ResultData.Result);

                    }



                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }

            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Active(int id , CategoryModelId category)
        {
            using (var client = new HttpClient())
            {

                try
                {

                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var data = new CategoryModelId()
                    {
                        CategoryId = id,
                        //IsActive = true,
                        //IsDelete = false,

                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<CategoryModelId>("api/Category/Active", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;


                        return RedirectToAction("Index", "Category");

                    }

                }
                catch (Exception ex)
                {

                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }


            }


            return View();
        }

    }
}
