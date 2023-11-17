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
        private dbEntities db = new dbEntities();

        // Đăng xuất
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }


        // Tạo view cho khách hàng Login
        public ActionResult Login()
        {
            //if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            //{
            //    ViewBag.username = Request.Cookies["username"];
            //    ViewBag.password = Request.Cookies["password"];
            //}
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
            // check là khách hàng cần tìm
            var check = db.Users.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check != null)  //không có KH
            {
                // Có tồn tại KH -> chuẩn bị dữ liệu đưa về lại ShowCart.cshtml
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = check.ID;
                Session["DiaChi"] = check.DiaChi;
                Session["UserName"] = check.NameUser;
                Session["FullName"] = check.FullName;
                Session["Phone"] = check.PhoneNumber;
                // Quay lại trang giỏ hàng với thông tin cần thiết
                return RedirectToAction("ProductList", "Products");
            }
            else
            {   
                //ViewBag.ErrorInfo = "Đăng nhập sai, xin hãy đăng nhập lại !!!";
                return View("Login");
            }



            //if (ModelState.IsValid)
            //{
            //    //if (string.IsNullOrEmpty(_user.NameUser))
            //    //    ModelState.AddModelError(string.Empty, "Username không được để trống");
            //    //if (string.IsNullOrEmpty(_user.PasswordUser))
            //    //    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");

            //    if (ModelState.IsValid)
            //    {
            //        var check = db.Users.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            //        if (check != null)
            //        {
            //            ViewBag.ErrorInfo = "Dang nhap thanh cong";

            //            //db.Configuration.ValidateOnSaveEnabled = false;
            //            Session["ID"] = check.ID;
            //            Session["DiaChi"] = check.DiaChi;
            //            Session["UserName"] = check.NameUser;
            //            Session["FullName"] = check.FullName;
            //            Session["Phone"] = check.PhoneNumber;
            //            return RedirectToAction("Index", "Home");
            //        }

            //    }
            //}
            //else
            //{
            //    ViewBag.ErrorInfo = "Đăng nhập sai, xin hãy đăng nhập lại !!!";
            //    return View("LoginCus");
            //}
            //return View();

        }




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
            if (ModelState.IsValid)
            {
                if (_user.PhoneNumber.Length < 10 || _user.PhoneNumber.Length > 10)
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại chỉ bao gồm 10 chữ số");
                }
                if (ModelState.IsValid)
                {
                    var check_name = db.Users.Where(n => n.PhoneNumber == _user.PhoneNumber).FirstOrDefault();
                    if (check_name == null) // chưa có tên khách hàng
                    {
                        //db.Configuration.ValidateOnSaveEnabled = false;
                        db.Users.Add(_user);
                        db.SaveChanges();
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        ViewBag.ErrorRegister = "Số điện thoại này đã tồn tại !";
                        return View();
                    }
                }
            }
            return View();
        }

    }
}