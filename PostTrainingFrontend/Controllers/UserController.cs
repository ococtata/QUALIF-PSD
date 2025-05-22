using PostTrainingFrontend.Models;
using PostTrainingFrontend.Models.Common;
using PostTrainingFrontend.UserWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTrainingFrontend.Controllers
{
    public class UserController
    {
        private String err = "";

        private UserWebService.UserWebService userWS = new UserWebService.UserWebService();
        public Response<User> Login(String email, String password)
        {

            if (email.Equals("") || password.Equals(""))
            {
                err = "Input cannot be empty";
            }

            if (err == "")
            {
                String jsonResponse = userWS.Login(email, password);
                return Json.Decode<Response<User>>(jsonResponse);
            }

            return new Response<User>
            {
                Success = false,
                Message = err,
                Payload = null,
            };
        }

        public Response<User> Register(String name, String email, String password)
        {
            if (name.Equals("") || email.Equals("") || password.Equals(""))
            {
                err = "Input cannot be empty";
            }
            else if (!email.Contains("@") || !email.Contains(".com"))
            {
                err = "Email must be valid format (contains \"@\" and \".com\")";
            }
            else if (password.Length < 6)
            {
                err = "Password length must be at least 6 characters";
            }

            if (err == "")
            {
                string jsonResponse = userWS.Register(name, email, password);
                return Json.Decode<Response<User>>(jsonResponse);
            }

            return new Response<User>
            {
                Success = false,
                Message = err,
                Payload = null,
            };
        }

        public Response<User> LoginUserByCookie(string cookie)
        {
            System.Diagnostics.Debug.WriteLine("Cookie: " + cookie);
            if (cookie == null)
            {
                return new Response<User>
                {
                    Success = false,
                    Message = "Cookie is null",
                    Payload = null,
                };
            }

            string jsonResponse = userWS.LoginUserByCookie(cookie);
            return Json.Decode<Response<User>>(jsonResponse);
        }
    }
}