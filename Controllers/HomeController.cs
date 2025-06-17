using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u24687473_HW01.Models;

namespace u24687473_HW01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
        public ActionResult RideHistory() => View();
    }
}