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
                        sailors.Add(@sailor);
                    }
                }
            }
            return sailors;
        }
    }
}
