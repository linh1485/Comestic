using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo1_mp.Models;

namespace demo1_mp.Controllers
{
    public class ProductController : Controller
    {
        private dbEntities db = new dbEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}