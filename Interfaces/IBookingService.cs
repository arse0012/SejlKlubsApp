using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SejlKlubsApp.Models;

namespace SejlKlubsApp.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> SearchBookingBySailorId(int id);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task BookBoatAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
        Task<Booking> GetBookingByIdAsync(int id);
        Task<List<Booking>> GetBookingsBySailorId(int sailorId);
    }
}
