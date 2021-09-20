using SejlKlubsApp.Models;
using SejlKlubsApp.Services.ADO_Service;
using SejlKlubsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SejlKlubsApp.Services.Service
{
    public class BookingService : IBookingService
    {
        private ADO_BookingService bookingService { get; set; }
        public BookingService(ADO_BookingService service)
        {
            bookingService = service;
        }
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await bookingService.GetAllBookingsAsync();
        }
        public async Task BookBoatAsync(Booking booking)
        {
            await bookingService.BookBoat(booking);
        }

        public async Task DeleteBookingAsync(Booking booking)
        {
            await bookingService.DeleteBookingAsync(booking);
        }

        public Task<IEnumerable<Booking>> SearchBookingBySailorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Booking>> GetBookingsBySailorId(int sailorId)
        {
            return await bookingService.GetBookingbySailorIdAsync(sailorId);
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await bookingService.GetBookingByIdAsync(id);
        }
    }
}
