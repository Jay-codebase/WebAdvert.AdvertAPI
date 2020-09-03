using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebAdvert.AdvertAPI.Services;

namespace WebAdvert.AdvertAPI.HealthCheck
{
    public class StorageHealthcheck : IHealthCheck
    {
        private readonly IAdvertStorageService _storageService;

        public StorageHealthcheck(IAdvertStorageService storageService)
        {
            _storageService = storageService;

        }
        public   Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isstorageok = true;
            
            if (isstorageok)
                return Task.FromResult(HealthCheckResult.Healthy(""));
            else
                return Task.FromResult(HealthCheckResult.Unhealthy(""));

        }
    }
}
