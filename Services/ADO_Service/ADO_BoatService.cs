using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SejlKlubsApp.Models;

namespace SejlKlubsApp.Services.ADO_Service
{
    public class ADO_BoatService
    {
        private List<Boat> boats;
        private string connectionString;
        public IConfiguration Configuration { get; }

        public ADO_BoatService(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            boats = new List<Boat>();
        }

        public async Task<List<Boat>> GetAllBoatsAsync()
        {
            string sql = "Select * From Boat";
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (await dataReader.ReadAsync())
                    {
                        Boat @boat = new Boat();
                        @boat.BoatId = Convert.ToInt32(dataReader["BoatId"]);
                        @boat.BoatType = Convert.ToString(dataReader["BoatType"]);
                        @boat.Color = Convert.ToString(dataReader["Color"]);
                        @boat.Condition = Convert.ToString(dataReader["Condition"]);
                        @boat.ImageName = Convert.ToString(dataReader["ImageName"]);
                        boats.Add(@boat);
                    }
                }
            }
            return boats;
        }

        public async Task NewBoatAsync(Boat boat)
        {
            string sql = $"Insert Into Boat(BoatType, Color, Condition, ImageName) Values (@BoatType, @Color, @Condition, @ImageName)";
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BoatType", boat.BoatType);
                    command.Parameters.AddWithValue("@Color", boat.Color);
                    command.Parameters.AddWithValue("@Condition", boat.Condition);
                    command.Parameters.AddWithValue("@ImageName", boat.ImageName);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteBoatAsync(Boat boat)
        {
            string sql = $"Delete From Boat Where BoatId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", boat.BoatId);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Boat> GetBoatById(int id)
        {
            Boat @boat = new Boat();
            string sql = $"Select * From Boat Where BoatId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        @boat.BoatId = Convert.ToInt32(dataReader["BoatId"]);
                        @boat.BoatType = Convert.ToString(dataReader["BoatType"]);
                        @boat.Color = Convert.ToString(dataReader["Color"]);
                        @boat.Condition = Convert.ToString(dataReader["Condition"]);
                        @boat.ImageName = Convert.ToString(dataReader["ImageName"]);
                    }
                }
            }
            return @boat;
        }

        public async Task EditBoatAsync(Boat boat)
        {
            string sql = $"Update Boat Set (BoatType, Color, Condition, ImageName) Values (@BoatType, @Color, @Condition, @ImageName) Where BoatId=@BoatId";
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", boat.BoatId);
                    command.Parameters.AddWithValue("@BoatType", boat.BoatType);
                    command.Parameters.AddWithValue("@Color", boat.Color);
                    command.Parameters.AddWithValue("@Condition", boat.Condition);
                    command.Parameters.AddWithValue("@ImageName", boat.ImageName);
                    int affectedRows =  await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
