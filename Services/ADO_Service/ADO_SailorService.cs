using Microsoft.Extensions.Configuration;
using SejlKlubsApp.Exceptions;
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
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try 
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        while (await dataReader.ReadAsync())
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
                            sailors.Add(@sailor);
                        }
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                    
                }
            }
            return sailors;
        }
        public async Task<List<Sailor>> GetSailorByNameAsync(string name)
        {
            string sql = $"Select * From Sailor Where Name LIKE'" + @name + "%" + "'";
            await using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@name", name);
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        while (await dataReader.ReadAsync())
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
                            sailors.Add(@sailor);
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }                   
                }
            }
            return sailors;
        }
        public async Task<bool> NewSailorAsync(Sailor sailor)
        {
            string sql = $"Insert Into Sailor(Name, LastName, Age, Phone, Email, Password, Admin) Values(@Name, @LastName, @Age, @Phone, @Email, @Password, @Admin)";
            await using(SqlConnection connection = new SqlConnection(connectionString))
            {
                
                await using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("Name", sailor.Name);
                    command.Parameters.AddWithValue("LastName", sailor.LastName);
                    command.Parameters.AddWithValue("Age", sailor.Age);
                    command.Parameters.AddWithValue("Phone", sailor.Phone);
                    command.Parameters.AddWithValue("Email", sailor.Email);
                    command.Parameters.AddWithValue("Password", sailor.Password);
                    command.Parameters.AddWithValue("Admin", sailor.Admin);
                    if (EmailExist(sailor.Email))
                    {
                        throw new ExistsException("Email bruges allerede");
                    }
                    await command.Connection.OpenAsync();
                    int affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        private bool EmailExist(string email)
        {
            foreach(Sailor s in GetAllSailorsAsync().Result)
            {
                if (s.Email == email)
                    return true;
            }
            return false;
        }
        public async Task<Sailor> DeleteSailorAsync(Sailor sailor)
        {
            string sql = $"Delete From Sailor Where SailorId=@id";
            await using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@id", sailor.SailorId);
                        int affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows == 1)
                        {
                            return sailor;
                        }
                    }
                    catch(Exception e)
                    {
                        return null;
                    }                    
                }            
            }
            return null;
        }
        public async Task<Sailor> GetSailorByIdAsync(int id)
        {
            Sailor @sailor = new Sailor();
            string sql = $"Select * From Sailor Where SailorId=@id";
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@id", id);
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        if (dataReader.Read())
                        {
                            sailor.SailorId = Convert.ToInt32(dataReader["SailorId"]);
                            sailor.Name = Convert.ToString(dataReader["Name"]);
                            sailor.LastName = Convert.ToString(dataReader["LastName"]);
                            sailor.Age = Convert.ToInt32(dataReader["Age"]);
                            sailor.Phone = Convert.ToString(dataReader["Phone"]);
                            sailor.Email = Convert.ToString(dataReader["Email"]);
                            sailor.Password = Convert.ToString(dataReader["Password"]);
                            sailor.Admin = Convert.ToBoolean(dataReader["Admin"]);
                        }
                        else
                        {
                            sailor = null;
                        }
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                    
                }
            }
            return @sailor;
        }
        public async Task<bool> EditSailorAsync(Sailor sailor)
        {
            string sql = $"Update Sailor Set Name=@Name, LastName=@LastName, Age=@Age, Phone=@Phone, Email=@Email, Password=@Password Where SailorId=@id";
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {             
                await using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@id", sailor.SailorId);
                    command.Parameters.AddWithValue("@Name", sailor.Name);
                    command.Parameters.AddWithValue("@LastName", sailor.LastName);
                    command.Parameters.AddWithValue("@Age", sailor.Age);
                    command.Parameters.AddWithValue("@Phone", sailor.Phone);
                    command.Parameters.AddWithValue("@Email", sailor.Email);
                    command.Parameters.AddWithValue("@Password", sailor.Password);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                    if(affectedRows == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
