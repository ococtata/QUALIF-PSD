using PostTraining.Application.Common;
using PostTraining.Application.Handlers;
using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PostTraining.WebServices
{
    /// <summary>
    /// Summary description for UserWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserWebService : System.Web.Services.WebService
    {

        private UserHandler userHandler = new UserHandler();

        [WebMethod]
        public String Login(String email, String password)
        {
            return Json.Encode(userHandler.Login(email, password));
        }

        [WebMethod]
        public String Register(String name, String email, String password)
        {
            return Json.Encode(userHandler.Register(name, email, password));
        }

        [WebMethod]
        public String LoginUserByCookie(String cookie)
        {
            return Json.Encode(userHandler.LoginUserByCookie(cookie));
        }
    }
}
