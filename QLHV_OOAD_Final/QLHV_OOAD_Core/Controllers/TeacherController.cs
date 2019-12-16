using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QLHV_OOAD_Core.Models;

namespace QLHV_OOAD_Core.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IConfiguration configuration;
        public static GiaoVien giaoVien;
        public static MonHoc monHoc;
        public static List<HocSinh> ListHocSinh;
        public static Lop lop;
        public static Users userTemp;
        public TeacherController(IConfiguration config)
        {
            this.configuration = config;

        }
        public IActionResult TeacherView(Users user)
        {

            if (HttpContext.Session.GetString("SessionUser") == null) return RedirectToAction("ValidateForm", "Validation");

            userTemp = user;
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");

            //Lấy thông tin Giáo viên
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select *from GiaoVien where IDGV = '" + user.ID + "'";
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                giaoVien = new GiaoVien();
                giaoVien.ID = Convert.ToInt32(dr["ID"]);
                giaoVien.IDGV = dr["IDGV"].ToString();
                giaoVien.Hoten = dr["HoTen"].ToString();
                giaoVien.TenMonHoc = dr["TenMonHoc"].ToString();
                giaoVien.SDT = dr["SDT"].ToString();
                giaoVien.MatKhau = dr["MatKhau"].ToString();
            }
            con.Close();

            //Lấy tên môn dạy
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select [TenMonHoc] from MonHoc where ID = '" + giaoVien.TenMonHoc + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                monHoc = new MonHoc();
                monHoc.TenMonHoc = dr["TenMonHoc"].ToString();
            }
            con.Close();

            //Truyền dữ liệu vào view
            ViewData["Teacher"] = giaoVien;
            ViewData["Mon"] = monHoc;
            return View();
        }

        public IActionResult ScoreAdd()
        {
            List<HocSinh> ListHocSinh = new List<HocSinh>(); 
            //Trả về khi hết phiên session
            if (HttpContext.Session.GetString("SessionUser") == null) return RedirectToAction("ValidateForm", "Validation");

            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");
            string myString = 12.ToString("000");

            //Lấy dữ liệu HocSinh Giáo viên dạy
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select HocSinh.HoTen, Lop.IDL " +
                "from HocSinh inner join LopDay on HocSinh.TenLop = LopDay.IDL " +
                "inner join Lop on Lop.ID = LopDay.IDL " +
                "where LopDay.IDGV = '"+ giaoVien.ID +"'";
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                HocSinh hocSinh = new HocSinh();
                hocSinh.HoTen = dr["HoTen"].ToString();
                hocSinh.TenLop = dr["IDL"].ToString();
                ListHocSinh.Add(hocSinh);
            }
            con.Close();

            //Lấy dữ liệu của học kỳ đang có
            List<string> hocky = new List<string>();
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select IDHK from HocKy Group By IDHK";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                hocky.Add(dr["IDHK"].ToString());
            }
            con.Close();
            ViewData["Student"] = ListHocSinh;
            ViewData["HocKy"] = hocky;
            return View();
        }

        public IActionResult GetScoreAdd(string Ten, string Year, string HK , KiemTra test)
        {
            int value = 0; //value của tổng các IDKT

            SqlDataReader dr = null;
            SqlCommand cmd = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");
            string myString = 12.ToString("000");


            //Tự tạo ra IDKT
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select COUNT(IDKT) from KiemTra";
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                value = Convert.ToInt32(dr[""]);
            }
            value = value + 1;
            con.Close();
            string formatValue = value.ToString("000");
            string IDKT = "KT" + formatValue;

            //Lấy IDHS
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;

            string id= "";
            cmd.CommandText = "Select [ID] from HocSinh where HoTen = N'"+Ten+"'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id = dr["ID"].ToString();
            }
            con.Close();

            //Add vào bảng điểm
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;

            cmd.CommandText = "Insert into KiemTra(IDKT,TenMonHoc,HinhThuc,NgayKT,Diem,IDHK,IDHS) " +
                "values(@Id, @tenMon, @hinhThuc, @ngayKT, @diem, @idhk, @idhs)";

            cmd.Parameters.AddWithValue("@Id", IDKT);
            cmd.Parameters.AddWithValue("@tenMon", giaoVien.TenMonHoc);
            cmd.Parameters.AddWithValue("@HinhThuc", test.HinhThuc);
            cmd.Parameters.AddWithValue("@ngayKT", test.NgayKT);
            cmd.Parameters.AddWithValue("@diem", test.Diem);
            cmd.Parameters.AddWithValue("@idhk", HK);
            cmd.Parameters.AddWithValue("@idhs", id);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                TempData["Add"] = "Sucess";
            }
            catch (Exception ex)
            {
                con.Close();
                TempData["Add"] = "Fail";
            }



            return RedirectToAction("ScoreAdd", "Teacher",TempData["Add"]);
        }

        public IActionResult ScoreEdit()
        {
            ListHocSinh = new List<HocSinh>();
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");
            SqlCommand cmd = null;

            //Chọn học sinh cần sửa
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select HocSinh.HoTen, Lop.IDL " +
                "from HocSinh inner join LopDay on HocSinh.TenLop = LopDay.IDL " +
                "inner join Lop on Lop.ID = LopDay.IDL " +
                "where LopDay.IDGV = '" + giaoVien.ID + "'";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HocSinh hocSinh = new HocSinh();
                hocSinh.HoTen = dr["HoTen"].ToString();
                hocSinh.TenLop = dr["IDL"].ToString();
                ListHocSinh.Add(hocSinh);
            }
            con.Close();

            List<string> hocky = new List<string>();
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select IDHK from HocKy Group By IDHK";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                hocky.Add(dr["IDHK"].ToString());
            }
            con.Close();
            ViewData["Student"] = ListHocSinh;
            ViewData["HocKy"] = hocky;

            return View();
        }

        public IActionResult GetScoreEdit(string Ten, string Year, string HK, KiemTra test)
        {
            SqlDataReader dr = null;
            SqlCommand cmd = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");

            //Lấy IDHS
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;

            string id = "";
            cmd.CommandText = "Select [ID] from HocSinh where HoTen = N'" + Ten + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id = dr["ID"].ToString();
            }
            con.Close();

            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;

            cmd.CommandText = "Update KiemTra set Diem = @diem where IDHS = @idhs and IDHK = @idhk and HinhThuc = @hinhthuc";
            cmd.Parameters.AddWithValue("@diem", test.Diem);
            cmd.Parameters.AddWithValue("@idhs", id);
            cmd.Parameters.AddWithValue("@idhk", HK);
            cmd.Parameters.AddWithValue("@hinhthuc", test.HinhThuc);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                TempData["Edit"] = "Sucess";
            }

            catch(Exception ex)
            {
                TempData["Edit"] = "Fail";
            }


            return RedirectToAction("ScoreEdit", "Teacher", TempData["Edit"]);
        }

        public IActionResult SuaDiemData()
        {
            return RedirectToAction("ScoreEdit", "Teacher", giaoVien);
        }

        public IActionResult ThemDiemData()
        {
            return RedirectToAction("ScoreAdd", "Teacher", giaoVien);
        }

        public IActionResult ThongTinData(Users user)
        {
            user = userTemp;
            return RedirectToAction("TeacherView", "Teacher", user);
        }
    }
}