using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Domain.Common.CommonClass;
using System.Text;
using WebSite.Helpers;
using Domain.Entities;

namespace WebSite.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var stringContent = new StringContent("", Encoding.UTF8, "application/json"); 


                    ResponseStandardJson<UserModel> ResData = (ResponseStandardJson<UserModel>)
                                                              JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                              (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ResData.Result.Token);
                    client.BaseAddress = new Uri(CommonWeb.UrlApi);


                    var postTask = await client.PostAsJsonAsync("api/Product/GetList", stringContent);

                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<List<ProductModel>> ResultData = (ResponseStandardJson<List<ProductModel>>)
                                                                               JsonConvert.DeserializeObject(result.ToString(),
                                                                               (typeof(ResponseStandardJson<List<ProductModel>>)));

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

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    ResponseStandardJson<UserModel> t = (ResponseStandardJson<UserModel>)
                                                        JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                        (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", t.Result.Token);

                    var data = new ProductModelId()
                    {
                        ProductId = id
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<ProductModelId>("api/Product/GetById", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<ProductModel> ResultData = (ResponseStandardJson<ProductModel>)
                                                                          JsonConvert.DeserializeObject(result.ToString(),
                                                                          (typeof(ResponseStandardJson<ProductModel>)));

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

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductModelCreate product)
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


                    var data = new ProductModelCreate()
                    {
                        ProductPrice = product.ProductPrice,
                        ProductName = product.ProductName,
                        CategoryId = product.CategoryId,
                        
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<ProductModelCreate>("api/Product/Add", data);


                    if (postTask.IsSuccessStatusCode)
                    {

                        TempData["SuccessMessage"] = "New Product Created";
                        return RedirectToAction("Index", "Product");
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

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    ResponseStandardJson<UserModel> t = (ResponseStandardJson<UserModel>)
                                                        JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                        (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", t.Result.Token);

                    var data = new ProductModelId()
                    {
                        ProductId = id
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<ProductModelId>("api/Product/GetById", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<ProductModel> ResultData = (ResponseStandardJson<ProductModel>)
                                                                          JsonConvert.DeserializeObject(result.ToString(),
                                                                          (typeof(ResponseStandardJson<ProductModel>)));

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

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductModelUpdate product)
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


                    var data = new ProductModelUpdate()
                    {
                       ProductId = id,
                       ProductName = product.ProductName,
                       ProductPrice = product.ProductPrice,
                       CategoryId = product.CategoryId,
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<ProductModelUpdate>("api/Product/Update", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;

                        return RedirectToAction("Index", "Product");

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

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    ResponseStandardJson<UserModel> t = (ResponseStandardJson<UserModel>)
                                                        JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                        (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", t.Result.Token);

                    var data = new ProductModelId()
                    {
                        ProductId = id
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<ProductModelId>("api/Product/GetById", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<ProductModel> ResultData = (ResponseStandardJson<ProductModel>)
                                                                          JsonConvert.DeserializeObject(result.ToString(),
                                                                          (typeof(ResponseStandardJson<ProductModel>)));

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

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ProductModelId product)
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


                    var data = new ProductModelId()
                    {
                        ProductId = id,

                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<ProductModelId>("api/Category/Delete", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;

                        return RedirectToAction("Index", "Product");

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
                    ResponseStandardJson<UserModel> t = (ResponseStandardJson<UserModel>)
                                                        JsonConvert.DeserializeObject(Request.Cookies["_CookDataResult"].ToString(),
                                                        (typeof(ResponseStandardJson<UserModel>)));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", t.Result.Token);

                    var data = new ProductModelId()
                    {
                        ProductId = id
                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<ProductModelId>("api/Product/GetById", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<ProductModel> ResultData = (ResponseStandardJson<ProductModel>)
                                                                          JsonConvert.DeserializeObject(result.ToString(),
                                                                          (typeof(ResponseStandardJson<ProductModel>)));

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
        public async Task<ActionResult> Active(int id, ProductModelId product)
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


                    var data = new ProductModelId()
                    {
                        ProductId = id,
                        

                    };

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<ProductModelId>("api/Product/Active", data);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;


                        return RedirectToAction("Index", "Product");

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
