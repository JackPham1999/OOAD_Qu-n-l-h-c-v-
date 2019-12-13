using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLHV_OOAD_Core.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace QLHV_OOAD_Core.Controllers
{
   
    public class ValidationController : Controller
    {
        
        private readonly IConfiguration configuration;
        public const string GV_Char = "GV";
        public static Users userData = new Users();

        [NonAction]
        public bool checkID(string id)
        {
            int i = 0;
            string s = id;
            bool result = int.TryParse(s, out i);
            return result;
        }

        public ValidationController(IConfiguration config)
        {
            this.configuration = config;
        }
        public IActionResult ValidateForm()
        {
            if ((string)HttpContext.Session.GetString("SessionUser") != null)
            {
                var session = this.HttpContext.Session.GetString("SessionUser");
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userData));
                return RedirectToAction("StudentView", "Student",userData);
            }
            return View();
        }


        public IActionResult Login(Users user)
        {
            
                
            if (user.HoTen == null || user.ID == null) return RedirectToAction("ValidateForm");
            SqlDataReader dr;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");

            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if(checkID(user.ID) == true)
            {
                cmd.CommandText = "Select *from HocSinh where HoTen = N'" + user.HoTen + "' and IDHS = N'" + user.ID + "'";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    userData.ID = user.ID;
                    userData.HoTen = user.HoTen;
                    var session = this.HttpContext.Session.GetString("SessionUser");
                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));
                    return RedirectToAction("StudentView","Student", user, session);
                    //return Content(HttpContext.Session.GetString("SessionUser"));
                }

            }

            else
            {
                if(user.ID.Substring(0,GV_Char.Length) == GV_Char)
                {
                    cmd.CommandText = "Select *from GiaoVien where HoTen = N'" + user.HoTen + "' and IDGV = N'" + user.ID + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        return Content(user.HoTen + " Giao Vien");
                    }
                }
            }
            con.Close();
            TempData["LoginError"] = "Error";
            return RedirectToAction("ValidateForm", TempData["LoginError"]);
        }

    }

}




