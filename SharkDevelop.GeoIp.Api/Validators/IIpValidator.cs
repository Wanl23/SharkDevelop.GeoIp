using System.Threading.Tasks;

namespace SharkDevelop.GeoIp.Api.Validators
{
    public interface IIpValidator
    {
        Task<bool> IsIpValidAsync(string ipAddress);
    }
}