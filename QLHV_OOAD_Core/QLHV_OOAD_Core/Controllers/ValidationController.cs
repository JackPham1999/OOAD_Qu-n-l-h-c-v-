using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QLHV_OOAD_Core.Controllers
{
    public class ValidationController : Controller
    {
        public IActionResult ValidateForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            return Content("A");
        }
    }
}