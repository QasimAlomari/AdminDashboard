using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class UserModel
    {
        [JsonIgnore]   
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }

    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterModel
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
