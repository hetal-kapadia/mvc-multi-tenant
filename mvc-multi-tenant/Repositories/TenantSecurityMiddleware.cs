using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_multi_tenant.Repositories
{
    public class TenantSecurityMiddleware
    {
        private readonly RequestDelegate next;

        public TenantSecurityMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            string tenantIdentifier = context.Session.GetString("TenantId");

            if (string.IsNullOrWhiteSpace(tenantIdentifier))
            {
                var apiKey = context.Request.Headers["X-Api-Key"].FirstOrDefault();

                if (string.IsNullOrWhiteSpace(apiKey))
                    return;

                if (!Guid.TryParse(apiKey, out Guid apiKeyGuid))
                    return;

                TenantRepository tenantRepository = new TenantRepository(configuration, httpContextAccessor);
                string tenantId = await tenantRepository.GetTenantId(apiKeyGuid);

                context.Session.SetString("TenantId", tenantId);
            }

            await next.Invoke(context);
        }
    }
}
