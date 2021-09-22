using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SejlKlubsApp.Models
{
    public class Sailor
    {
        public int SailorId { get; set; }
        [Required(ErrorMessage = "Navn skal fyldes ud")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Efternavn skal fyldes ud")]
        public string LastName { get; set; }
        public int Age { get; set; }
        [Required(ErrorMessage = "Angiv telefon nummer")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Angiv mailaddresse")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Kodeord skal fyldes ud")]
        public string Password { get; set; }
        [BindProperty, DataType(DataType.Password), Display(Name = "Password")]
        public string PasswordCheck { get; set; }
        [Required(ErrorMessage = "Status skal sættes")]
        public bool Member { get; set; }
        [Required(ErrorMessage = "Status skal sættes")]
        public bool Admin { get; set; }

    }
}
