using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using u24687473_HW01.Models;

namespace u24687473_HW01.Controllers
{
    public class BookingController : Controller
    {
        public static List<Booking> _bookings = new List<Booking>();
        public static List<Driver> _drivers = new List<Driver>();
        public static List<Vehicle> _vehicles = new List<Vehicle>();

     
        static BookingController()
        {
            
            _drivers = new List<Driver>
    {
        
        new Driver { Id = "DR-ALS1", Name = "Perle Kabelenge", Phone = "083 288 1293", Certification = "ALS", ServiceType = "Advanced Life Support", ImageUrl = "/Images/Paramedic1.png" },
        new Driver { Id = "DR-ALS2", Name = "Bilaal Abrahams", Phone = "092 238 1239", Certification = "ALS", ServiceType = "Advanced Life Support", ImageUrl = "/Images/Paramedic2.png" },
        
       
        new Driver { Id = "DR-BLS1", Name = "Saskia Naidoo", Phone = "011 123 4566", Certification = "BLS", ServiceType = "Basic Life Support", ImageUrl = "/Images/Paramedic3.png" },
        new Driver { Id = "DR-BLS2", Name = "Yasmeka Naidoo", Phone = "023 143 3555", Certification = "BLS", ServiceType = "Basic Life Support", ImageUrl = "/Images/Paramedic5.png"},
        
        
        new Driver { Id = "DR-PS1", Name = "Doomy Pather", Phone = "045 487 4867", Certification = "PS", ServiceType = "Patient Support", ImageUrl = "/Images/Paramedic7.png" },
        new Driver { Id = "DR-PS2", Name = "Nish Maharaj", Phone = "045 456 4807", Certification = "PS", ServiceType = "Patient Support", ImageUrl = "/Images/Paramedic1.png" },
        
       
        new Driver { Id = "DR-MUV1", Name = "Amy Kromm", Phone = "029 784 7365", Certification = "MUV", ServiceType = "Medical Utility Vehicle", ImageUrl = "/Images/Paramedic6.png"},
        new Driver { Id = "DR-MUV2", Name = "Bryce Orchard", Phone = "029 984 1643", Certification = "MUV", ServiceType = "Medical Utility Vehicle", ImageUrl = "/Images/Paramedic4.png" },
        
        
        new Driver { Id = "DR-EMA1", Name = "Veer Gosai", Phone = "072 784 7635", Certification = "EMA", ServiceType = "Event Medical Ambulance", ImageUrl = "/Images/Paramedic2.png" },
        new Driver { Id = "DR-EMA2", Name = "Chaydin Govender", Phone = "023 764 8335", Certification = "EMA", ServiceType = "Event Medical Ambulance", ImageUrl = "/Images/Paramedic4.png" },
        
       
        new Driver { Id = "DR-AA1", Name = "Roarke Bailey", Phone = "098 718 9205", Certification = "AA", ServiceType = "Air Ambulance", ImageUrl = "/Images/Paramedic6.png" },
        new Driver { Id = "DR-AA2", Name = "Pabi Prinsloo", Phone = "011 778 0023", Certification = "AA", ServiceType = "Air Ambulance",ImageUrl = "/Images/Paramedic5.png" }
    };

            
            _vehicles = new List<Vehicle>
    {
        
        new Vehicle { Id = "VH-ALS1", Type = "ALS Ambulance 1", Registration = "ALS-001", ServiceType = "Advanced Life Support", ImageUrl = "/Images/Ambulance.jpg" },
        new Vehicle { Id = "VH-ALS2", Type = "ALS Ambulance 2", Registration = "ALS-002",ServiceType = "Advanced Life Support", ImageUrl = "/Images/Ambulance.jpg" },
        
        
        new Vehicle { Id = "VH-BLS1", Type = "BLS Ambulance 1", Registration = "BLS-001",  ServiceType = "Basic Life Support", ImageUrl = "/Images/Ambulance.jpg"  },
        new Vehicle { Id = "VH-BLS1", Type = "BLS Ambulance 2", Registration = "BLS-002",  ServiceType = "Basic Life Support",ImageUrl = "/Images/Ambulance.jpg"  },
        
        
        new Vehicle { Id = "VH-PS1", Type = "PS Ambulance 1", Registration = "PS-001",  ServiceType = "Patient Support", ImageUrl = "/Images/Ambulance.jpg"  },
        new Vehicle { Id = "VH-PS1", Type = "PS Ambulance 1", Registration = "PS-002",  ServiceType = "Patient Support", ImageUrl = "/Images/Ambulance.jpg"  },
        
        
        new Vehicle { Id = "VH-MUV1", Type = "MUV Ambulance 1", Registration = "MUV-001", ServiceType = "Medical Utility Vehicle", ImageUrl = "/Images/Ambulance.jpg"  },
        new Vehicle { Id = "VH-MUV1", Type = "MUV Ambulance 2", Registration = "MUV-002", ServiceType = "Medical Utility Vehicle", ImageUrl = "/Images/Ambulance.jpg" },
        
       
        new Vehicle { Id = "VH-EMA1", Type = "EMA Ambulance 1", Registration = "EMA-001",  ServiceType = "Event Medical Ambulance", ImageUrl = "/Images/Ambulance.jpg"  },
        new Vehicle { Id = "VH-EMA1", Type = "EMA Ambulance 2", Registration = "EMA-002",  ServiceType = "Event Medical Ambulance", ImageUrl = "/Images/Ambulance.jpg"  },
        
       
        new Vehicle { Id = "VH-AA1", Type = "AA Helicopter 1", Registration = "AA-001", ServiceType = "Air Ambulance", ImageUrl = "/Images/AirAmbulance.jpg"  },
        new Vehicle { Id = "VH-AA1", Type = "AA Helicopter 2", Registration = "AA-002", ServiceType = "Air Ambulance", ImageUrl = "/Images/AirAmbulance.jpg"  }
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
              
                model.Driver = _drivers.FirstOrDefault(d => d.Id == model.DriverId);
                model.Vehicle = _vehicles.FirstOrDefault(v => v.Id == model.VehicleId);

                model.Id = Guid.NewGuid().ToString();
                model.BookingDate = DateTime.Now;
                _bookings.Add(model);
                return RedirectToAction("BookingConfirmed", new { id = model.Id });
            }

        
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
                ContactNumber = "10111",
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

        public ActionResult SelectService()
        {
            return View();
        }

        public ActionResult CreateSOSBooking()
        {
            return View();
        }
    }
}