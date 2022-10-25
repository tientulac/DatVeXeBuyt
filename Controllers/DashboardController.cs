using QLXeBuyt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLXeBuyt.Controllers
{
    public class DashboardController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
    }
}