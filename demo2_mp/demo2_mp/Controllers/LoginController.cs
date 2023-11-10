using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demo2_mp.Models;

namespace demo2_mp.Controllers
{
    public class LoginController : Controller
    {
        private dbEntities2 db = new dbEntities2();

        // Đăng xuất
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }


        // Tạo view cho khách hàng Login
        public ActionResult Login()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.username = Request.Cookies["username"];
                ViewBag.password = Request.Cookies["password"];
            }
            return View();
        }

        //
        //public void ghinhotk(string username, string password)
        //{
        //    HttpCookie us = new HttpCookie("username");
        //    HttpCookie pw = new HttpCookie("password");
        //    us.Value = username;
        //    pw.Value = password;
        //    us.Expires = DateTime.Now.AddDays(1);
        //    pw.Expires = DateTime.Now.AddDays(1);
        //    Response.Cookies.Add(us);
        //    Response.Cookies.Add(pw);

        //}


        // Xử lý tìm kiếm UserName, password và thông báo
        [HttpPost]
        public ActionResult LoginAcount(User _user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(_user.NameUser))
                    ModelState.AddModelError(string.Empty, "Username không được để trống");
                if (string.IsNullOrEmpty(_user.PasswordUser))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");

                if (ModelState.IsValid)
                {
                    var check = db.Users.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
                    if (check != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        Session["ID"] = check.ID;
                        Session["DiaChi"] = check.DiaChi;
                        Session["UserName"] = check.NameUser;
                        Session["FullName"] = check.FullName;
                        Session["Phone"] = check.PhoneNumber;
                        //var listgroup = Get
                        //Session.Add("SESSION_GROUP",)

                        return RedirectToAction("Index", "Home");

                    }
                    else ViewBag.ErrorInfo = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View();

        }

            //// check là khách hàng cần tìm
            //var check = db.Users.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            //if (check == null)  //không có KH
            //{
            //    ViewBag.ErrorInfo = "Đăng nhập sai, xin hãy đăng nhập lại !!!";
            //    return View("Login");
            //}
            //else
            //{   // Có tồn tại KH -> chuẩn bị dữ liệu đưa về lại ShowCart.cshtml
            //    db.Configuration.ValidateOnSaveEnabled = false;
            //    Session["IDCus"] = check.ID;
            //    Session["Password"] = check.PasswordUser;
            //    Session["NameCus"] = check.FullName;
            //    Session["PhoneCus"] = check.PhoneNumber;
            //    Session["UserName"] = check.NameUser;
            //    // Quay lại trang giỏ hàng với thông tin cần thiết
            //    return RedirectToAction("ProductList", "Products");
            //}
        

        //public List<string> GetListGroupID(string username)
        //{
        //    var data = (from a in db.UserGroups
        //                join b in db.Users
        //                where b.NameUser == username

        //                select new
        //                {
        //                    UserGroupID = b.ID,
        //                    UserGroupName = a.name
        //                });
        //    return data.Select(x => x.UserGroupName).ToList();
        //}



        // Regíter
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User _user)
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(_user.FullName))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(_user.Email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                //if (string.IsNullOrEmpty(_user.PhoneNumber))
                //    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                //if (string.IsNullOrEmpty(_user.Gender))
                //    ModelState.AddModelError(string.Empty, "");
                //if (string.IsNullOrEmpty(_user.DiaChi))
                //    ModelState.AddModelError(string.Empty, "");
                if (string.IsNullOrEmpty(_user.NameUser))
                    ModelState.AddModelError(string.Empty, "Username không được để trống");
                if (string.IsNullOrEmpty(_user.PasswordUser))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");

                //
                var check_name = db.Users.Where(n => n.FullName == _user.FullName).FirstOrDefault();
                if (check_name != null) // da có tên khách hàng
                {
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tên này");
                }
                if (ModelState.IsValid)
                {
                    
                    db.Users.Add(_user);
                    db.SaveChanges();
                }
                else return View();
            }
            return RedirectToAction("Login");
        }

    }
}