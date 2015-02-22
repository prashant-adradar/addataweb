using System;

namespace AdradarAdDataWeb.Models
{
    public interface IServiceClientFactory
    {
        AdradarAdDataWeb.AdDataServiceReference.IAdDataService GetAdDataServiceClient();
    }
}
