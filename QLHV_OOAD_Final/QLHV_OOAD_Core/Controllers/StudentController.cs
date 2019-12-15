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


        public static HocSinh hsTemp;
        public static Users userTemp;
        public IActionResult StudentView(Users user, HocSinh hs, PhuHuynh[] phList)
        {
            if (HttpContext.Session.GetString("SessionUser") == null) return RedirectToAction("ValidateForm", "Validation");
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");
            userTemp = user;

            //Lấy dữ liệu HocSinh
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            string getLop = "";

            cmd.CommandText = "Select *from HocSinh where IDHS = '" + user.ID + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                hs.IDHS = Convert.ToInt32(dr["ID"]);
                hs.HoTen = dr["HoTen"].ToString();
                hs.TenLop = dr["TenLop"].ToString();
                hs.DCHT = dr["DCHT"].ToString();
                hs.DCTT = dr["DCTT"].ToString();
                hs.DanToc = dr["DanToc"].ToString();
                hs.CheDo = dr["CheDo"].ToString();
                hs.SDT = dr["SDT"].ToString();
                hs.GioiTinh = dr["GioiTinh"].ToString();
                hs.NgaySinh = Convert.ToDateTime(dr["NgaySinh"]);

            }
            hsTemp = hs;
            con.Close();

            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select [IDL] from Lop where ID = '" + hs.TenLop + "'";
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                getLop = dr["IDL"].ToString();
            }
            con.Close();
            //Lấy dữ liệu QuanHe, Phu Huynh
            phList = new PhuHuynh[5];
            con.Open();
            cmd.Connection = con;
            int count = 0;
            cmd.CommandText = "Select [TenQuanHe],[SDT],[NgheNghiep],[HoTen] " +
                "from QuanHe Inner Join PhuHuynh on PhuHuynh.ID = QuanHe.IDPH " +
                "where IDHS = '" + hs.IDHS + "'";
            dr = cmd.ExecuteReader();
            while (dr.Read() || count < 3)
            {
                PhuHuynh ph = new PhuHuynh();
                try
                {
                    if (dr["TenQuanHe"].ToString() == "Mẹ")
                    {
                        ph.SDT = dr["SDT"].ToString();
                        ph.NgheNghiep = dr["NgheNghiep"].ToString();
                        ph.HoTen = dr["HoTen"].ToString();
                    }

                    else if (dr["TenQuanHe"].ToString() == "Cha")
                    {
                        ph.SDT = dr["SDT"].ToString();
                        ph.NgheNghiep = dr["NgheNghiep"].ToString();
                        ph.HoTen = dr["HoTen"].ToString();
                    }

                    else if (dr["TenQuanHe"].ToString() == "Giám Hộ")
                    {
                        ph.SDT = dr["SDT"].ToString();
                        ph.NgheNghiep = dr["NgheNghiep"].ToString();
                        ph.HoTen = dr["HoTen"].ToString();
                    }

                    else
                    {
                        ph.SDT = "Không có";
                        ph.HoTen = "Không có";
                        ph.NgheNghiep = "Không có";
                    }


                }
                catch (Exception e)
                {
                    ph.SDT = "Không có";
                    ph.HoTen = "Không có";
                    ph.NgheNghiep = "Không có";
                    phList[count] = ph;
                    count++;
                }

                phList[count] = ph;
                count++;
            }

            con.Close();

            //Dựa vào dữ liệu quan hệ lấy ra thông tin phụ huynh

            ViewData["Class"] = getLop;
            ViewData["Student"] = hs;
            ViewData["Parent"] = phList;
            return View();
        }

        public static List<string> hk = new List<string>();
        public IActionResult Semester(Users user,String Year)
        {
            
            if (HttpContext.Session.GetString("SessionUser") == null) return RedirectToAction("ValidateForm", "Validation");
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");


            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select [IDHK] from HocKy where IDHS = '" + hsTemp.IDHS + "'";
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                string hocky = dr["IDHK"].ToString().Substring(3,4);
                if (!hk.Contains(hocky))
                {
                    hk.Add(hocky);
                }
            }
            con.Close();

            ViewData["Student"] = hsTemp;
            ViewData["HocKy"] = hk;
            return View();
        }


        [NonAction]
        public void AddDataDiem(Users user, String Year)
        {

        }

        [HttpPost]
        public IActionResult GetDiem(Users user, String Year)
        {
            int soMon = 9;
            int soCotDiem = 3;
            string[,] Diem_1 = new string[soMon,soCotDiem];
            string[,] Diem_2 = new string[soMon, soCotDiem];
            SetDefaultValue(Diem_1, Diem_2, soMon, soCotDiem);


            SqlDataReader dr = null;
            SqlCommand cmd = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");

            for (int mon = 0; mon < soMon; mon++)
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select *from KiemTra where IDHS = '" + hsTemp.IDHS + "'and IDHK like '%" + Year + "' and TenMonHoc = '" + (mon+1) + "'";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //Lay diem HK1
                    if (dr["IDHK"].ToString().Contains("HK1"))
                    {
                        if(dr["HinhThuc"].ToString() == "Miệng")
                        {
                            Diem_1[mon, 0] = dr["Diem"].ToString();
                        }

                        if (dr["HinhThuc"].ToString() == "GK")
                        {
                            Diem_1[mon, 1] = dr["Diem"].ToString();
                        }

                        if (dr["HinhThuc"].ToString() == "CK")
                        {
                            Diem_1[mon, 2] = dr["Diem"].ToString();
                        }
                    }

                    //Lay Diem HK2
                    if (dr["IDHK"].ToString().Contains("HK2"))
                    {
                        if (dr["HinhThuc"].ToString() == "Miệng")
                        {
                            Diem_2[mon, 0] = dr["Diem"].ToString();
                        }

                        if (dr["HinhThuc"].ToString() == "GK")
                        {
                            Diem_2[mon, 1] = dr["Diem"].ToString();
                        }

                        if (dr["HinhThuc"].ToString() == "CK")
                        {
                            Diem_2[mon, 2] = dr["Diem"].ToString();
                        }
                    }
                }
                con.Close();
            }
            ViewData["Diem_1"] = Diem_1;
            ViewData["Diem_2"] = Diem_2;
            ViewData["HocKy"] = hk;
            ViewData["Student"] = hsTemp;
            return View("Semester");
        }

        public IActionResult PostbackData(Users user )
        {
            user = userTemp;
            return RedirectToAction("StudentView", user);
        }

        [NonAction]
        public void SetDefaultValue(string[,] Diem_1, string[,] Diem_2, int mon, int cotDiem)
        {
            for(int i = 0; i < mon; i++)
            {
                for(int z = 0; z < cotDiem;z++)
                {
                    Diem_1[i, z] = "";
                    Diem_2[i, z] = "";
                }
            }
        }
    }
}