using System;
using System.Threading.Tasks;

namespace SharkDevelop.GeoIp.Api.Validators
{
    public class IpValidator : IIpValidator
    {
        public Task<bool> IsIpValidAsync(string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}