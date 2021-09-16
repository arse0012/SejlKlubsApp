using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SejlKlubsApp.Models;

namespace SejlKlubsApp.Services.Interfaces
{
    public interface ISailorService
    {
        Task<IEnumerable<Sailor>> GetSailorByNameAsync(string name);
        Task<IEnumerable<Sailor>> GetAllSailorsAsync();
        Task AddSailorAsync(Sailor sailor);
        Task DeleteSailorAsync(Sailor sailor);
        Task UpdateSailorAsync(Sailor sailor);
        Task<Sailor> GetSailorById(int id);
    }
}
