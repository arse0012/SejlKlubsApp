using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SejlKlubsApp.Models
{
    public class Boat
    {
        public int BoatId { get; set; }
        [Required(ErrorMessage = "Bådetype skal angives")]
        public string BoatType { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        [Required(ErrorMessage = "Insæt bådens billede")]
        public string ImageName { get; set; }

    }
}
