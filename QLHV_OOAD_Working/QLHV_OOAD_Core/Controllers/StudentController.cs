using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLHV_OOAD_Core.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace QLHV_OOAD_Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration configuration;


        public StudentController(IConfiguration config)
        {
            this.configuration = config;

        }
        public IActionResult StudentView(Users user)
        {
            if (HttpContext.Session.GetString("SessionUser") == null) return RedirectToAction("ValidateForm", "Validation");
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");

            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select *from HocSinh where IDHS = '"+user.ID+"'";
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                user.HoTen = dr["HoTen"].ToString();
                user.TenLop = dr["TenLop"].ToString();
                user.DiaChi = dr["DCHT"].ToString();
                user.SDT = dr["SDT"].ToString();
            }

            con.Close();


            ViewData["Student"] = user;
            return View();
        }

        public IActionResult Semester()
        {
            return View();
        }
    }
}