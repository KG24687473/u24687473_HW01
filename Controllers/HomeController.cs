using System.Web.Mvc;
using u24687473_HW01.Models;
using System.Linq;

namespace u24687473_HW01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RideHistory()
        {
           
            var bookings = BookingController.GetBookings()
                .Select(b => new Booking
                {
                    Id = b.Id,
                    ServiceType = b.ServiceType,
                    IsEmergency = b.IsEmergency,
                    BookingDate = b.BookingDate,
                    PickupTime = b.PickupTime,
                    Location = b.Location,
                    PatientName = b.PatientName,
                    ContactNumber = b.ContactNumber,
                    MedicalCondition = b.MedicalCondition,
                    Driver = b.Driver ?? new Driver { Name = b.IsEmergency ? "Emergency Response Team" : "Unknown Driver" },
                    Vehicle = b.Vehicle ?? new Vehicle { Type = b.IsEmergency ? "Emergency Response Unit" : "Unknown Vehicle" }
                })
                .OrderByDescending(b => b.BookingDate)
                .ToList();

            return View(bookings);
        }

        public ActionResult Manage()
        {

            return View();
        }

    }
}