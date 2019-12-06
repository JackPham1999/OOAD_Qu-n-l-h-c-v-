using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLHV_OOAD_Core.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;




namespace QLHV_OOAD_Core.Controllers
{
   
    public class ValidationController : Controller
    {
        
        private readonly IConfiguration configuration;
        public const string GV_Char = "GV";

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

        [HttpPost]
        public IActionResult Login(Users user)
        {
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
                    
                    return Content(user.HoTen + " Hoc Snh");
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
    }
}