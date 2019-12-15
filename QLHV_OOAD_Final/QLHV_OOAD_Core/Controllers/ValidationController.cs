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
        public int checkID(string id)
        {
            if (id.Substring(0, 2) == "HS") return 1;
            if (id.Substring(0, 2) == "GV") return 2;
            return 0;
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
                if (session.Contains("GV")) 
                {
                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userData));
                    return RedirectToAction("TeacherView", "Teacher", userData);
                }
                else{
                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userData));
                    return RedirectToAction("StudentView", "Student", userData);
                }
            }
            return View();
        }


        public IActionResult Login(Users user)
        {
            SqlDataReader dr;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");

            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if(checkID(user.ID) == 1)
            {
                cmd.CommandText = "Select *from HocSinh where IDHS = N'" + user.ID + "' and MatKhau = N'" + user.MatKhau + "'";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    userData.ID = user.ID;
                    userData.HoTen = user.HoTen;
                    var session = this.HttpContext.Session.GetString("SessionUser");
                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));
                    con.Close();
                    return RedirectToAction("StudentView","Student", user, session);
                    //return Content(HttpContext.Session.GetString("SessionUser"));
                }

            }

            else
            {
                if(user.ID.Substring(0,GV_Char.Length) == GV_Char)
                {
                    cmd.CommandText = "Select *from GiaoVien where IDGV = N'" + user.ID + "' and MatKhau = N'" + user.MatKhau + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        userData.ID = user.ID;
                        userData.HoTen = user.HoTen;
                        var session = this.HttpContext.Session.GetString("SessionUser");
                        HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));
                        con.Close();
                        return RedirectToAction("TeacherView", "Teacher", user, session);
                    }
                }
            }
            con.Close();
            TempData["LoginError"] = "Error";
            return RedirectToAction("ValidateForm", TempData["LoginError"]);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("ValidateForm", "Validation");
        }

    }

}




