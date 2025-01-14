﻿using System;
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
        public IActionResult StudentView(Users user, HocSinh hs, PhuHuynh[] phList)
        {
            if (HttpContext.Session.GetString("SessionUser") == null) return RedirectToAction("ValidateForm", "Validation");
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");


            //Lấy dữ liệu HocSinh
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

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

            ViewData["Student"] = hs;
            ViewData["Parent"] = phList;
            return View();
        }

        public IActionResult Semester(Users user)
        {
            if (HttpContext.Session.GetString("SessionUser") == null) return RedirectToAction("ValidateForm", "Validation");
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = configuration.GetConnectionString("QLHVContext");


            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select *from HocKy where IDHS = '" + user.ID + "'";
            dr = cmd.ExecuteReader();

            return View();
        }
    }
}