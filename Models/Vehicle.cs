using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u24687473_HW01.Models
{
    public class Vehicle
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string LicensePlate { get; set; }
        public string ServiceType { get; set; }
        public string ImagePath { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}