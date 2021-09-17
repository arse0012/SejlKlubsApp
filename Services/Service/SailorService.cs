using SejlKlubsApp.Models;
using SejlKlubsApp.Services.ADO_Service;
using SejlKlubsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SejlKlubsApp.Services.Service
{
    public class SailorService : ISailorService
    {
        private ADO_SailorService sailorService { get; set; }
        public SailorService(ADO_SailorService service)
        {
            sailorService = service;
        }
        public async Task<IEnumerable<Sailor>> GetAllSailorsAsync()
        {
            return await sailorService.GetAllSailorsAsync();
        }
        public async Task<IEnumerable<Sailor>> GetSailorByNameAsync(string name)
        {
            return await sailorService.GetSailorByNameAsync(name);
        }
        public async Task AddSailorAsync(Sailor sailor)
        {
            await sailorService.NewSailorAsync(sailor);
        }

        public async Task DeleteSailorAsync(Sailor sailor)
        {
            await sailorService.DeleteSailorAsync(sailor);
        }

        public async Task<Sailor> GetSailorByIdAsync(int id)
        {
            return await sailorService.GetSailorByIdAsync(id);
        }

        public async Task UpdateSailorAsync(Sailor sailor)
        {
            await sailorService.EditSailorAsync(sailor);
        }
    }
}
