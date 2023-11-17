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
    public class AdminController : Controller
    {
        private dbEntities db = new dbEntities();

        // Đăng xuất
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAcount(Admin _admin)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(_admin.NameUser))
                    ModelState.AddModelError(string.Empty, "User name không được để trống");
                if (string.IsNullOrEmpty(_admin.PasswordUser))
                    ModelState.AddModelError(string.Empty, "Password không được để trống");
                //Kiểm tra có admin này hay chưa
                var adminDB = db.Admins.FirstOrDefault(ad => ad.NameUser ==
                _admin.NameUser && ad.PasswordUser == _admin.PasswordUser);
                if (adminDB == null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                else
                {
                    Session["Admin"] = adminDB;
                    Session["AdminName"] = adminDB.FullName;
                    ViewBag.ThongBao = "Đăng nhập admin thành công";
                    return RedirectToAction("IndexAdmin", "Home");
                }
            }
            return View();

        }
    }
}