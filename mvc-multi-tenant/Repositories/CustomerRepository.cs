using Microsoft.Extensions.Configuration;
using mvc_multi_tenant.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_multi_tenant.Repositories
{
    public class CustomerRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Customer>> GetAllCustomers(string tenantId)
        {
            try
            {
                List<Customer> customers = new List<Customer>();

                using (var connection = new SqlConnection(_configuration.GetConnectionString("TenantDbConnection")))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SELECT * FROM Customers WHERE TenantId = @tenantId", connection))
                    {
                        command.Parameters.AddWithValue("@tenantId", tenantId);

                        var reader = await command.ExecuteReaderAsync();
                        if (reader.Read())
                        {
                            Customer customer = new Customer()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                TenantId = Guid.Parse(reader["TenantId"].ToString()),
                                CustomerName = reader["Id"].ToString(),
                                IsActive = bool.Parse(reader["IsActive"].ToString())
                            };

                            customers.Add(customer);
                        }

                        if (!reader.IsClosed)
                            await reader.CloseAsync();
                    }

                    if (connection.State != ConnectionState.Closed)
                        await connection.CloseAsync();

                    return customers;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
