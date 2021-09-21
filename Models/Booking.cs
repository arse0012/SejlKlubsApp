using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SejlKlubsApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        [BindProperty]
        public DateTime DateFrom { get; set; }
        [BindProperty]
        public DateTime DateTo { get; set; }
        public int BoatId { get; set; }
        [Required(ErrorMessage = "Sailor must be set")]
        public int SailorId { get; set; }
    }
}
