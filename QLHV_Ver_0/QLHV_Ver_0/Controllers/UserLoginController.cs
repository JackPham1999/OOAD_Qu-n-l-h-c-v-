using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLHV_Ver_0.Models;

namespace QLHV_Ver_0.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(Models.UserLogin user)
        {
            return Content(user.Name);
        }
    }
}