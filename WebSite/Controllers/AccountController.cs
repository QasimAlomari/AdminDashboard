using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using WebSite.Helpers;
using static Domain.Common.CommonClass;

namespace WebSite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Login(UserLoginModel collection)
        //{

        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage(HttpMethod.Post, CommonWeb.UrlApi + "api/User/Login");
        //    var content = new StringContent
        //                            ("{\r\n  \"userName\": \"qasim\",\r\n  \"password\": \"123\"\r\n}",
        //                             null, "application/json");

        //    request.Content = content;
        //    var response = await client.SendAsync(request);
        //    response.EnsureSuccessStatusCode();
        //    Console.WriteLine();
        //    var data = await response.Content.ReadAsStringAsync();
        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> Login(/*[FromBody]*/ UserLoginModel collection)
        {

            using (var client = new HttpClient())
            {
                try
                {

                    client.BaseAddress = new Uri(CommonWeb.UrlApi);
                    var postTask = await client.PostAsJsonAsync<UserLoginModel>("api/User/Login", collection);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var result = postTask.Content.ReadAsStringAsync().Result;
                        ResponseStandardJson<UserModel> ResultData = (ResponseStandardJson<UserModel>)JsonConvert.DeserializeObject(result.ToString(), (typeof(ResponseStandardJson<UserModel>)));
                        
                        CookieOptions optionUsers = new CookieOptions();
                        optionUsers.Expires = DateTime.Now.AddDays(1);

                        Response.Cookies.Append("_CookDataResult", JsonConvert.SerializeObject(ResultData), optionUsers);

                        return RedirectToAction("Index", "Category");
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View();
           
        }
    }
}

//                    

