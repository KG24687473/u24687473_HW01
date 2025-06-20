using System;
using System.Web.Mvc;
using u24687473_HW01.Models;

namespace u24687473_HW01.Controllers
{
    public class BookingController : Controller
    {
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
                model.DriverId = AssignDriver(model);
                model.VehicleId = AssignVehicle(model);

                // Changed to return BookingConfirmed view
                return RedirectToAction("BookingConfirmed", new { id = model.Id });
            }

            ViewBag.ServiceType = model.ServiceType;
            return View("Booking", model);
        }

        // Changed action name to match your view
        public ActionResult BookingConfirmed(string id)
        {
            var booking = GetBookingById(id) ?? new Booking
            {
                Id = id,
                BookingDate = DateTime.Now,
                ServiceType = "Basic Life Support",
                PatientName = "Test Patient",
                ContactNumber = "(123) 456-7890",
                Location = "123 Main Street",
                MedicalCondition = "Non-emergency transport",
                DriverId = "DRV-" + Guid.NewGuid().ToString().Substring(0, 5),
                VehicleId = "VH-" + Guid.NewGuid().ToString().Substring(0, 5)
            };

            return View(booking);
        }

        public ActionResult Emergency()
        {
            var booking = new Booking
            {
                Id = Guid.NewGuid().ToString(),
                BookingDate = DateTime.Now,
                IsEmergency = true,
                ServiceType = "Advanced Life Support",
                PatientName = "EMERGENCY PATIENT",
                ContactNumber = "911",
                Location = "GPS COORDINATES PENDING",
                MedicalCondition = "LIFE-THREATENING CONDITION",
                DriverId = "DRV-EMER-01",
                VehicleId = "VH-EMER-01"
            };

            // Changed to return BookingConfirmed view
            return RedirectToAction("BookingConfirmed", new { id = booking.Id });
        }

        private string AssignDriver(Booking booking)
        {
            return booking.IsEmergency
                ? "DRV-EMER-" + DateTime.Now.Minute.ToString("D2")
                : "DRV-" + Guid.NewGuid().ToString().Substring(0, 5);
        }

        private string AssignVehicle(Booking booking)
        {
            return booking.IsEmergency
                ? "VH-EMER-" + DateTime.Now.Minute.ToString("D2")
                : "VH-" + Guid.NewGuid().ToString().Substring(0, 5);
        }

        private Booking GetBookingById(string id)
        {
            // In real app, implement database lookup
            return null;
        }
    }
}