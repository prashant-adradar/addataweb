using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdradarAdDataWeb.AdDataServiceReference;

namespace AdradarAdDataWeb.Models
{
    public class ServiceClientFactory : IServiceClientFactory
    {
        public IAdDataService GetAdDataServiceClient()
        {
            return new AdDataServiceClient();
        }
    }
}