using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo1_mp.Models;


namespace demo1_mp.Controllers
{
    public class CategoryController : Controller
    {
        private dbEntities db = new dbEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

    }
}