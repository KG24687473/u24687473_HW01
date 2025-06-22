using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u24687473_HW01.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public string ServiceType { get; set; }
        public bool IsEmergency { get; set; }
        public DateTime BookingDate { get; set; }
        public string PickupTime { get; set; }
        public string Location { get; set; }
        public string PatientName { get; set; }
        public string ContactNumber { get; set; }
        public string MedicalCondition { get; set; }

        public string DriverId { get; set; }
        public Driver Driver { get; set; }

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}