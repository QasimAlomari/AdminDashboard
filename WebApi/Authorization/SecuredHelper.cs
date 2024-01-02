using Nancy.Json;
using static Domain.Common.CommonClass;

namespace WebApi.Authorization
{
    public class SecuredHelper
    {
        public static JwtAuthResponce GetInfoFromToken(string Token)
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] source = Token.Split(new string[] { "." }, 
                                            StringSplitOptions.RemoveEmptyEntries);

            JwtAuthResponce jwtResponce = 
                serializer.Deserialize<JwtAuthResponce>(source[1]);

            //jwtResponce.nameid = (jwtResponce.nameid);
            //jwtResponce.unique_name = (jwtResponce.unique_name);

            return jwtResponce;
        }


    }
}
