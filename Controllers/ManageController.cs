using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using u24687473_HW01.Models;

namespace u24687473_HW01.Controllers
{
    public class ManageController : Controller
    {
        public ActionResult Manage(string searchName, string searchService)
        {
            var drivers = BookingController._drivers;
            var vehicles = BookingController._vehicles;

            if (!string.IsNullOrEmpty(searchName))
                drivers = drivers.Where(d => d.Name.IndexOf(searchName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (!string.IsNullOrEmpty(searchService))
                drivers = drivers.Where(d => d.ServiceType.Equals(searchService, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Drivers = drivers;
            ViewBag.Vehicles = vehicles;
            ViewBag.Services = BookingController._drivers.Select(d => d.ServiceType).Distinct().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult AddDriver(Driver driver)
        {
            driver.Id = Guid.NewGuid().ToString();
            BookingController._drivers.Add(driver);
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public ActionResult UpdateDriver(Driver updatedDriver)
        {
            var existingDriver = BookingController._drivers.FirstOrDefault(d => d.Id == updatedDriver.Id);
            if (existingDriver != null)
            {
                existingDriver.Name = updatedDriver.Name;
                existingDriver.Phone = updatedDriver.Phone;
                existingDriver.Certification = updatedDriver.Certification;
                existingDriver.ServiceType = updatedDriver.ServiceType;
                existingDriver.ImageUrl = updatedDriver.ImageUrl;
            }
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public ActionResult DeleteDriver(string id)
        {
            var driver = BookingController._drivers.FirstOrDefault(d => d.Id == id);
            if (driver != null)
                BookingController._drivers.Remove(driver);

            return RedirectToAction("Manage");
        }

        [HttpPost]
        public ActionResult AddVehicle(Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid().ToString();
            BookingController._vehicles.Add(vehicle);
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public ActionResult UpdateVehicle(Vehicle updatedVehicle)
        {
            var existingVehicle = BookingController._vehicles.FirstOrDefault(v => v.Id == updatedVehicle.Id);
            if (existingVehicle != null)
            {
                existingVehicle.Type = updatedVehicle.Type;
                existingVehicle.Registration = updatedVehicle.Registration;
                existingVehicle.ServiceType = updatedVehicle.ServiceType;
                existingVehicle.ImageUrl = updatedVehicle.ImageUrl;
            }
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public ActionResult DeleteVehicle(string id)
        {
            var vehicle = BookingController._vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle != null)
                BookingController._vehicles.Remove(vehicle);

            return RedirectToAction("Manage");
        }

        public FileResult ExportVehicles()
        {
            var lines = BookingController._vehicles.Select(v =>
                $"{v.Id} | {v.Type} | {v.Registration} | {v.ServiceType} | {v.Features}");

            var filePath = Server.MapPath("~/App_Data/VehiclesExport.txt");
            System.IO.File.WriteAllLines(filePath, lines);

            return File(filePath, "text/plain", "VehiclesExport.txt");
        }
    }
}
