using Microsoft.Extensions.Configuration;
using SejlKlubsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SejlKlubsApp.Services.ADO_Service
{
    public class ADO_BookingService
    {
        private List<Booking> bookings;
        private string connectionString;
        public IConfiguration Configuration { get; }
        public ADO_BookingService(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            bookings = new List<Booking>();
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            string sql = "Select * From Booking";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        while (await dataReader.ReadAsync())
                        {
                            Booking @booking = new Booking();
                            @booking.BookingId = Convert.ToInt32(dataReader["BookingId"]);
                            @booking.BoatId = Convert.ToInt32(dataReader["BoatId"]);
                            @booking.SailorId = Convert.ToInt32(dataReader["SailorId"]);
                            @booking.DateFrom = Convert.ToDateTime(dataReader["DateFrom"]);
                            @booking.DateTo = Convert.ToDateTime(dataReader["DateTo"]);
                            bookings.Add(@booking);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
            return bookings;
        }
        public async Task<List<Booking>> GetBookingbySailorIdAsync(int id)
        {
            string sql = $"Select * From Booking Where SailorId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try 
                    {
                        command.Parameters.AddWithValue("@id", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        while (dataReader.Read())
                        {
                            Booking @booking = new Booking();
                            @booking.BookingId = Convert.ToInt32(dataReader["BookingId"]);
                            @booking.BoatId = Convert.ToInt32(dataReader["BoatId"]);
                            @booking.SailorId = Convert.ToInt32(dataReader["SailorId"]);
                            @booking.DateFrom = Convert.ToDateTime(dataReader["DateFrom"]);
                            @booking.DateTo = Convert.ToDateTime(dataReader["DateTo"]);
                            bookings.Add(@booking);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
            return bookings;
        }
        public async Task<bool> BookBoat(Booking booking)
        {
            string sql = $"Insert Into Booking(BoatId, SailorId, DateFrom, DateTo) Values(@BoatId, @SailorId, @DateFrom, @DateTo)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@BoatId", booking.BoatId);
                        command.Parameters.AddWithValue("@SailorId", booking.SailorId);
                        command.Parameters.AddWithValue("@DateFrom", booking.DateFrom);
                        command.Parameters.AddWithValue("@DateTo", booking.DateTo);
                        int affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows == 1)
                        {
                            return true;
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                    return false;
                }
            }
        }
        public async Task<Booking> DeleteBookingAsync(Booking booking)
        {
            string sql = $"Delete From Booking Where BookingId=@id";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@id", booking.BookingId);
                        int affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows == 1)
                        {
                            return booking;
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
                return null;
            }
        }
        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            Booking @booking = new Booking();
            string sql = $"Select * From Booking Where BookingId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@id", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        while (dataReader.Read())
                        {
                            @booking.BookingId = Convert.ToInt32(dataReader["BookingId"]);
                            @booking.BoatId = Convert.ToInt32(dataReader["BoatId"]);
                            @booking.SailorId = Convert.ToInt32(dataReader["SailorId"]);
                            @booking.DateFrom = Convert.ToDateTime(dataReader["DateFrom"]);
                            @booking.DateTo = Convert.ToDateTime(dataReader["DateTo"]);
                            return @booking;
                        }
                    }
                    catch(SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                    return null;
                }
            }
        }
    }
}
