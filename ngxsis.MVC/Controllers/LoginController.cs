using ngxsis.ViewModel;
using ngxsis.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(LoginViewModel userModel)
        {
            using (var db = new ngxsisContext())
            {
                var userDetails = db.x_addrbook
                    .Where(o => o.email == userModel.email || o.abuid == userModel.email &&
                    o.abpwd == userModel.abpwd).FirstOrDefault();
                if (userDetails == null)
                {
                   
                }
            }
            return View();
        }
    }
}