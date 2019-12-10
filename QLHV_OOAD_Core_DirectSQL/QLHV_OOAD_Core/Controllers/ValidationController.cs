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
            return View();
        }


        public IActionResult Login(Users user)
        {
            if ((string)HttpContext.Session.GetString("SessionUser") != null) return Content("User still logged in");
                
            if (user.HoTen == null || user.ID == null) return RedirectToAction("ValidateForm");
            //string connectionString = configuration.GetConnectionString("DefaultConnectionString");
            SqlDataReader dr;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=NGUYEN-NGUYEN;Initial Catalog=HocVu;Integrated Security=True";

            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if(checkID(user.ID) == true)
            {
                cmd.CommandText = "Select *from HocSinh where HoTen = '" + user.HoTen + "' and IDHS = '" + user.ID + "'";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));
                    return RedirectToAction("StudentView","Student", user);
                    //return RedirectToAction("Test", user);
                    //return Content(user.HoTen + " Hoc Snh");
                    //return Content(HttpContext.Session.GetString("SessionUser"));
                }

            }

            else
            {
                if(user.ID.Substring(0,GV_Char.Length) == GV_Char)
                {
                    cmd.CommandText = "Select *from GiaoVien where HoTen = '" + user.HoTen + "' and IDGV = '" + user.ID + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        return Content(user.HoTen + " Giao Vien");
                    }
                }
            }
            con.Close();
            return RedirectToAction("ValidateForm");
        }


        public IActionResult Test(Users user)
        {
            if (user.ID == "17520334") return Content("Pass User Success");
            if (HttpContext.Session.GetString("SessionUser") != null) return Content("A");
            return Content("B");
        }
    }



}




