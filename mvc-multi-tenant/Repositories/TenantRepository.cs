using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_multi_tenant.Repositories
{
    public class TenantRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        
        private readonly IConfiguration _configuration;
        
        public TenantRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetTenantId(Guid apiKey)
        {
            string tenantId = null;

            try
            {
                using (var connection = new SqlConnection(_configuration["ConnectionStrings:TenantDBConnection"]))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SELECT Id FROM Tenant WHERE ApiKey = @apiKey", connection))
                    {
                        command.Parameters.AddWithValue("@apiKey", apiKey);

                        var reader = await command.ExecuteReaderAsync();
                        if (reader.Read())
                        {
                            tenantId = reader["Id"].ToString();
                        }

                        if (!reader.IsClosed)
                            await reader.CloseAsync();
                    }

                    if (connection.State != ConnectionState.Closed)
                        await connection.CloseAsync();

                    return tenantId;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> GetTenantId()
        {
            return await Task.FromResult(_session.GetString("TenantId"));
        }
    }
}
