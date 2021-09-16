﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.Interfaces;

namespace SejlKlubsApp.Pages.Sailors
{
    public class GetAllSailorsModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string FilterCriteria { get; set; }
        public IEnumerable<Sailor> Sailors { get; private set; }
        ISailorService sailorService;
        public GetAllSailorsModel(ISailorService service)
        {
            sailorService = service;
        }
        public async Task OnGetAsync()
        {
            Sailors = await sailorService.GetAllSailorsAsync();
        }
    }
}