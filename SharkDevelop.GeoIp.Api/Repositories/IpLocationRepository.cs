using System;
using System.Threading.Tasks;

namespace SharkDevelop.GeoIp.Api.Repositories
{
    public class IpLocationRepository : IIpLocationRepository
    {
        public Task<string> GetCountryNameAsync(string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
