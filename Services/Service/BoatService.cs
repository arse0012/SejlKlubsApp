using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.ADO_Service;
using SejlKlubsApp.Services.Interfaces;

namespace SejlKlubsApp.Services.Service
{
    public class BoatService : IBoatService
    {
        private ADO_BoatService boatService { get; set; }

        public BoatService(ADO_BoatService service)
        {
            boatService = service;
        }
        public async Task<IEnumerable<Boat>> GetAllBoatsAsync()
        {
            return await boatService.GetAllBoatsAsync();
        }
        public async Task AddBoatAsync(Boat boat)
        {
            await boatService.NewBoatAsync(boat);
        }

        public async Task DeleteBoatAsync(Boat boat)
        {
            await boatService.DeleteBoatAsync(boat);
        }
        public async Task<Boat> GetBoatByIdAsync(int id)
        {
            return await boatService.GetBoatById(id);
        }

        public async Task UpdateBoatAsync(Boat boat)
        {
            await boatService.EditBoatAsync(boat);
        }
    }
}
