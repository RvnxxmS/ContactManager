﻿using System.Web.Mvc;

namespace ContactManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}