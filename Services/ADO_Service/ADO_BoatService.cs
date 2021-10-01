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
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
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
            return boats;
        }

        public async Task<List<Boat>> GetBoatByNameAsync(string name)
        {
            string sql = $"Select * From Boat Where BoatType LIKE'" + @name + "%" + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@name", name);
                        await command.Connection.OpenAsync();
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
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

            return boats;
        }

        public async Task<bool> NewBoatAsync(Boat boat)
        {
            string sql = $"Insert Into Boat(BoatType, Color, Condition, ImageName) Values (@BoatType, @Color, @Condition, @ImageName)";
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@BoatType", boat.BoatType);
                        command.Parameters.AddWithValue("@Color", boat.Color);
                        command.Parameters.AddWithValue("@Condition", boat.Condition);
                        command.Parameters.AddWithValue("@ImageName", boat.ImageName);
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

        public async Task<Boat> DeleteBoatAsync(Boat boat)
        {
            string sql = $"Delete From Boat Where BoatId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@id", boat.BoatId);
                        int affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows == 1)
                        {
                            return boat;
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
                return boat;
            }
        }

        public async Task<Boat> GetBoatById(int id)
        {
            Boat @boat = new Boat();
            string sql = $"Select * From Boat Where BoatId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
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
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return null;
                    }
                }
            }
            return @boat;
        }

        public async Task<bool> EditBoatAsync(Boat boat)
        {
            string sql = $"Update Boat Set BoatType=@BoatType, Color=@Color, Condition=@Condition, ImageName=@ImageName Where BoatId=@id";
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@id", boat.BoatId);
                        command.Parameters.AddWithValue("@BoatType", boat.BoatType);
                        command.Parameters.AddWithValue("@Color", boat.Color);
                        command.Parameters.AddWithValue("@Condition", boat.Condition);
                        command.Parameters.AddWithValue("@ImageName", boat.ImageName);
                        int affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows == 1)
                        {
                            return true;
                        }
                    }
                    catch (SqlException sqlex)
                    {
                        Console.WriteLine("Database Fejl");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return false;
                    }

                }
            }
            return false;
        }
    }
}
