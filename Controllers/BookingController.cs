using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u24687473_HW01.Models;

namespace u24687473_HW01.Controllers
{
    public class BookingController : Controller
    {
        public ActionResult SelectService() => View();

        public ActionResult Booking(string serviceType)
        {
            ViewBag.ServiceType = serviceType;
            return View();
        }

        public ActionResult BookingConfirmed(bool isEmergency = false)
        {
            ViewBag.IsEmergency = isEmergency;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSOSBooking()
        {
            return RedirectToAction("BookingConfirmed", new { isEmergency = true });
        }
    }
}