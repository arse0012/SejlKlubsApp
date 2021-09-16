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
        public Task AddSailorAsync(Sailor sailor)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSailorAsync(Sailor sailor)
        {
            throw new NotImplementedException();
        }

        public Task<Sailor> GetSailorById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sailor>> GetSailorByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSailorAsync(Sailor sailor)
        {
            throw new NotImplementedException();
        }
    }
}
