using Microsoft.Extensions.Configuration;
using SejlKlubsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SejlKlubsApp.Services.ADO_Service
{
    public class ADO_SailorService
    {
        private List<Sailor> sailors;
        private string connectionString;
        public IConfiguration Configuration { get;  }
        public ADO_SailorService(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            sailors = new List<Sailor>();
        }
        public async Task<List<Sailor>> GetAllSailorsAsync()
        {
            string sql = "Select * From Sailor";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader=await command.ExecuteReaderAsync())
                {
                    while(await dataReader.ReadAsync())
                    {
                        Sailor @sailor = new Sailor();
                        @sailor.SailorId = Convert.ToInt32(dataReader["SailorId"]);
                        @sailor.Name = Convert.ToString(dataReader["Name"]);
                        @sailor.LastName = Convert.ToString(dataReader["LastName"]);
                        @sailor.Age = Convert.ToInt32(dataReader["Age"]);
                        @sailor.Phone = Convert.ToString(dataReader["Phone"]);
                        @sailor.Email = Convert.ToString(dataReader["Email"]);
                        @sailor.Password = Convert.ToString(dataReader["Password"]);
                        @sailor.Admin = Convert.ToBoolean(dataReader["Admin"]);
                        sailor.Member = Convert.ToBoolean(dataReader["Member"]);
                        sailors.Add(@sailor);
                    }
                }
            }
            return sailors;
        }
        public async Task<List<Sailor>> GetSailorByNameAsync(string name)
        {
            string sql = $"Select * From Sailor Where Name LIKE'" + @name + "%" + "'";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@name", name);
                using(SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while(await dataReader.ReadAsync())
                    {
                        Sailor @sailor = new Sailor();
                        @sailor.SailorId = Convert.ToInt32(dataReader["SailorId"]);
                        @sailor.Name = Convert.ToString(dataReader["Name"]);
                        @sailor.LastName = Convert.ToString(dataReader["LastName"]);
                        @sailor.Age = Convert.ToInt32(dataReader["Age"]);
                        @sailor.Phone = Convert.ToString(dataReader["Phone"]);
                        @sailor.Email = Convert.ToString(dataReader["Email"]);
                        @sailor.Password = Convert.ToString(dataReader["Password"]);
                        @sailor.Admin = Convert.ToBoolean(dataReader["Admin"]);
                        @sailor.Member = Convert.ToBoolean(dataReader["Member"]);
                        sailors.Add(@sailor);
                    }
                }
            }
            return sailors;
        }
        public async Task NewSailorAsync(Sailor sailor)
        {
            string sql = $"Insert Into Sailor(Name, LastName, Age, Phone, Email, Password) Values(@Name, @LastName, @Age, @Phone, @Email, @Password)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("Name", sailor.Name);
                    command.Parameters.AddWithValue("LastName", sailor.LastName);
                    command.Parameters.AddWithValue("Age", sailor.Age);
                    command.Parameters.AddWithValue("Phone", sailor.Phone);
                    command.Parameters.AddWithValue("Email", sailor.Email);
                    command.Parameters.AddWithValue("Password", sailor.Password);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task DeleteSailorAsync(Sailor sailor)
        {
            string sql = $"Delete From Sailor Where SailorId=@id";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", sailor.SailorId);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<Sailor> GetSailorByIdAsync(int id)
        {
            Sailor @sailor = new Sailor();
            string sql = $"Select * From Sailor Where SailorId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        sailor.SailorId = Convert.ToInt32(dataReader["SailorId"]);
                        sailor.Name = Convert.ToString(dataReader["Name"]);
                        sailor.LastName = Convert.ToString(dataReader["LastName"]);
                        sailor.Age = Convert.ToInt32(dataReader["Age"]);
                        sailor.Phone = Convert.ToString(dataReader["Phone"]);
                        sailor.Email = Convert.ToString(dataReader["Email"]);
                        sailor.Password = Convert.ToString(dataReader["Password"]);
                        sailor.Admin = Convert.ToBoolean(dataReader["Admin"]);
                        sailor.Member = Convert.ToBoolean(dataReader["Member"]);
                    }
                }
            }
            return @sailor;
        }
        public async Task EditSailorAsync(Sailor sailor)
        {
            string sql = $"Update Sailor Set Name=@Name, LastName=@LastName, Age=@Age, Phone=@Phone, Email=@Email, Password=@Password Where SailorId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", sailor.SailorId);
                    command.Parameters.AddWithValue("@Name", sailor.Name);
                    command.Parameters.AddWithValue("@LastName", sailor.LastName);
                    command.Parameters.AddWithValue("@Age", sailor.Age);
                    command.Parameters.AddWithValue("@Phone", sailor.Phone);
                    command.Parameters.AddWithValue("@Email", sailor.Email);
                    command.Parameters.AddWithValue("@Password", sailor.Password);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
