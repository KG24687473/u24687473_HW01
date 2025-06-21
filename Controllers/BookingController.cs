using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using u24687473_HW01.Models;

namespace u24687473_HW01.Controllers
{
    public class BookingController : Controller
    {
        private static List<Booking> _bookings = new List<Booking>();
        private static List<Driver> _drivers = new List<Driver>();
        private static List<Vehicle> _vehicles = new List<Vehicle>();

        // Initialize data (could be moved to a database)
        static BookingController()
        {
            // Initialize drivers
            _drivers = new List<Driver>
            {
                new Driver { Id = "DR-ALS1", Name = "Dr. Sarah Johnson", Phone = "555-234-5678", Certification = "ALS", ServiceType = "Advanced Life Support" },
                new Driver { Id = "DR-ALS2", Name = "Dr. Michael Chen", Phone = "555-345-6789", Certification = "ALS", ServiceType = "Advanced Life Support" },
                // Add all other drivers...
            };

            // Initialize vehicles
            _vehicles = new List<Vehicle>
            {
                new Vehicle { Id = "VH-ALS1", Type = "ALS Ambulance", Registration = "ALS-001", Features = "ICU, Ventilator", ServiceType = "Advanced Life Support" },
                new Vehicle { Id = "VH-ALS2", Type = "Critical Care Unit", Registration = "ALS-002", Features = "Full ICU", ServiceType = "Advanced Life Support" },
                // Add all other vehicles...
            };
        }

        public ActionResult Booking(string serviceType)
        {
            var model = new Booking { ServiceType = serviceType };

            ViewBag.Drivers = _drivers
                .Where(d => d.ServiceType == serviceType)
                .Select(d => new SelectListItem
                {
                    Value = d.Id,
                    Text = $"{d.Name} ({d.Certification})"
                })
                .ToList();

            ViewBag.Vehicles = _vehicles
                .Where(v => v.ServiceType == serviceType)
                .Select(v => new SelectListItem
                {
                    Value = v.Id,
                    Text = $"{v.Type} ({v.Registration})"
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBooking(Booking model)
        {
            if (ModelState.IsValid)
            {
                // Attach the full driver and vehicle objects
                model.Driver = _drivers.FirstOrDefault(d => d.Id == model.DriverId);
                model.Vehicle = _vehicles.FirstOrDefault(v => v.Id == model.VehicleId);

                model.Id = Guid.NewGuid().ToString();
                model.BookingDate = DateTime.Now;
                _bookings.Add(model);
                return RedirectToAction("BookingConfirmed", new { id = model.Id });
            }

            // Repopulate dropdowns if validation fails
            ViewBag.Drivers = _drivers
                .Where(d => d.ServiceType == model.ServiceType)
                .Select(d => new SelectListItem
                {
                    Value = d.Id,
                    Text = $"{d.Name} ({d.Certification})"
                })
                .ToList();

            ViewBag.Vehicles = _vehicles
                .Where(v => v.ServiceType == model.ServiceType)
                .Select(v => new SelectListItem
                {
                    Value = v.Id,
                    Text = $"{v.Type} ({v.Registration})"
                })
                .ToList();

            return View("Booking", model);
        }

        public ActionResult BookingConfirmed(string id)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == id) ?? new Booking
            {
                Id = id,
                BookingDate = DateTime.Now,
                ServiceType = "Emergency Response",
                IsEmergency = true,
                PatientName = "EMERGENCY PATIENT",
                ContactNumber = "911",
                Location = "GPS COORDINATES PENDING",
                MedicalCondition = "LIFE-THREATENING CONDITION",
                Driver = new Driver { Name = "Emergency Response Team", Phone = "911 Dispatch" },
                Vehicle = new Vehicle { Type = "Emergency Response Unit", Registration = "EMER-001" }
            };

            return View(booking);
        }

        public static List<Booking> GetBookings()
        {
            return _bookings;
        }
    }
}