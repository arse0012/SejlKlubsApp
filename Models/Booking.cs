using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SejlKlubsApp.Models
{
    public class Booking
    {
        public int bookingId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int BoatId { get; set; }
        public int SailorId { get; set; }
    }
}
