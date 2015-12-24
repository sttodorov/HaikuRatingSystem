using HaikuRatingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaikuRatingSystem.Services.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var a = new HaikuData().Haikus.All().ToList();
            return View();
        }
    }
}
