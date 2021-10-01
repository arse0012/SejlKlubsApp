using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SejlKlubsApp.Models;

namespace SejlKlubsApp.Interfaces
{
    public interface IBoatService
    {
        Task<IEnumerable<Boat>> GetBoatByNameAsync(string name);
        Task<IEnumerable<Boat>> GetAllBoatsAsync();
        Task AddBoatAsync(Boat boat);
        Task DeleteBoatAsync(Boat boat);
        Task UpdateBoatAsync(Boat boat);
        Task<Boat> GetBoatByIdAsync(int id);

    }
}
