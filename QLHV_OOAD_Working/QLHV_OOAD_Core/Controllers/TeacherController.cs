﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QLHV_OOAD_Core.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult TeacherView()
        {
            return View();
        }
    }
}