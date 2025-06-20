using System;
using System.Collections.Generic;
using System.Web.Mvc;
using u24687473_HW01.Models;
using System.Linq;

namespace u24687473_HW01.Controllers
{
    public class BookingController : Controller
    {
        private static List<Booking> _bookings = new List<Booking>();

        public ActionResult SelectService()
        {
            return View();
        }

        public ActionResult Booking(string serviceType)
        {
            ViewBag.ServiceType = serviceType;
            return View(new Booking { ServiceType = serviceType });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBooking(Booking model)
        {
            if (ModelState.IsValid)
            {
                model.Id = string.IsNullOrEmpty(model.Id) ? Guid.NewGuid().ToString() : model.Id;
                model.BookingDate = DateTime.Now;
                _bookings.Add(model);
                return RedirectToAction("BookingConfirmed", new { id = model.Id });
            }
            ViewBag.ServiceType = model.ServiceType;
            return View("Booking", model);
        }

        [HttpGet]
        public ActionResult CreateSOSBooking()
        {
            return View(new Booking
            {
                Id = Guid.NewGuid().ToString(),
                IsEmergency = true,
                ServiceType = "Emergency Response"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Emergency(Booking model)
        {
            if (ModelState.IsValid)
            {
                model.Id = string.IsNullOrEmpty(model.Id) ? Guid.NewGuid().ToString() : model.Id;
                model.BookingDate = DateTime.Now;
                model.DriverId = "EMER-" + DateTime.Now.Minute.ToString("D2");
                model.VehicleId = "EMER-" + DateTime.Now.Second.ToString("D2");
                _bookings.Add(model);
                return RedirectToAction("BookingConfirmed", new { id = model.Id });
            }
            return View("CreateSOSBooking", model);
        }

        public ActionResult BookingConfirmed(string id)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == id) ?? new Booking
            {
                Id = id,
                BookingDate = DateTime.Now,
                ServiceType = "Emergency Response",
                PatientName = "EMERGENCY PATIENT",
                ContactNumber = "911",
                Location = "GPS COORDINATES PENDING",
                MedicalCondition = "LIFE-THREATENING CONDITION",
                DriverId = "DRV-EMER-01",
                VehicleId = "VH-EMER-01"
            };
            return View(booking);
        }

        public static List<Booking> GetBookings()
        {
            return _bookings;
        }
    }
}