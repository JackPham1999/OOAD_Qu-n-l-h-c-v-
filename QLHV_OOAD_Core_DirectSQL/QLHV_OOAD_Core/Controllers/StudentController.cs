using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLHV_OOAD_Core.Models;
using System.Data.SqlClient;

namespace QLHV_OOAD_Core.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult StudentView(Users user)
        {
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=NGUYEN-NGUYEN;Initial Catalog=HocVu;Integrated Security=True";

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
    }
}